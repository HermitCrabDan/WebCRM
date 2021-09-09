namespace WebCRM.Shared
{
    using System;
    using System.Linq;
    
    using WebCRM.Data;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    /// <summary>
    /// Base repository class for handling Create, Read, Update, and Delete operations
    /// </summary>
    /// <typeparam name="Model">data model</typeparam>
    /// <typeparam name="ViewModel">view model</typeparam>
    /// <author>Daniel Lee Graf</author>
    public abstract class CRMRepositoryBase<Model, ViewModel, Context>: ICRMRepository<Model,ViewModel>
        where ViewModel: ICRMViewModel<Model>,new()
        where Model: class,ICRMDataModel<Model>
        where Context: DbContext
    {
        protected ILogger _logger;

        protected Context _ctx;

        public CRMRepositoryBase(Context ctx) 
        {
            this._ctx = ctx;
        }

        public CRMRepositoryBase(ILogger logger, Context ctx)
        {
            this._logger = logger;
            this._ctx = ctx;
        }

        public virtual (bool, ViewModel) Create(ViewModel model, string userID)
        {
            if (model != null)
            {
                try
                {
                    var modelToAdd = model.GetBaseModel();
                    modelToAdd.LastUpdatedDate = DateTime.Now;
                    modelToAdd.LastUpdatedBy = userID;
                    _ctx
                        .Set<Model>()
                        .Add(modelToAdd);
                    var success = (_ctx.SaveChanges() > 0);
                    var viewModel = new ViewModel();
                    viewModel.SetModelValues(modelToAdd);
                    return (success, viewModel);
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to create {typeof(Model).Name}");
                    }
                    model.ValidationErrorMessages.Add(ex.ToString());
                }
            }
            return (false, model);
        }

        public virtual (bool, ViewModel) RetrieveById(int id)
        {
            var model = _ctx
                .Set<Model>()
                .Where(w => w.Id == id)
                .FirstOrDefault();
                
            var viewModel = new ViewModel();
            bool success = false;
            if (model != null)
            {
                viewModel.SetModelValues(model);
                success = true;
            }
            return (success, viewModel);
        }

        public virtual IEnumerable<ViewModel> Retrieve(Func<Model, bool> selector)
        {
            var data = _ctx
                .Set<Model>()
                .Where(selector)
                .ToList();
            var viewModelList = new List<ViewModel>();
            foreach (var model in data)
            {
                var viewModel = new ViewModel();
                viewModel.SetModelValues(model);
                viewModelList.Add(viewModel);
            }
            return viewModelList;
        }

        public virtual (bool, ViewModel) Update(ViewModel model, string userID)
        {
            if (model != null)
            {
                try
                {
                    var modelToUpdate = 
                        _ctx
                            .Set<Model>()
                            .Where(w => w.Id == model.Id)
                            .FirstOrDefault();
                    if (modelToUpdate != null)
                    {
                        modelToUpdate.RestrictedModelUpdate(model.GetBaseModel());
                        modelToUpdate.LastUpdatedDate = DateTime.Now;
                        modelToUpdate.LastUpdatedBy = userID;

                        _ctx
                            .Set<Model>()
                            .Update(modelToUpdate);
                        var success = _ctx.SaveChanges() > 0;
                        var viewModel = new ViewModel();
                        viewModel.SetModelValues(modelToUpdate);
                        return (success, viewModel);
                    }
                }
                catch (Exception ex) 
                { 
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to update {typeof(Model).Name}, Id:{model?.Id}");
                    }
                }
            }
            return (false, model);
        }

        public virtual bool Delete(int id, string UserID)
        {
            if (id > 0)
            {
                try
                {
                    var modelToDelete = 
                        _ctx
                            .Set<Model>()
                            .Where(w => w.Id == id)
                            .FirstOrDefault();
                    if (modelToDelete != null)
                    {
                        modelToDelete.DeletionDate = DateTime.Now;
                        modelToDelete.DeletionBy = UserID;
                        _ctx
                            .Set<Model>()
                            .Update(modelToDelete);
                        return (_ctx.SaveChanges() > 0);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to delete {typeof(Model).Name}, Id:{id}");
                    }
                }
            }
            return false;
        }
    }
}
