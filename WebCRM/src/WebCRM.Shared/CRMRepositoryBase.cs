namespace WebCRM.Shared
{
    using System;
    using System.Linq;
    using WebCRM.Data;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

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

        public virtual async Task<(bool, ViewModel)> CreateAsync(ViewModel model, string userID)
        {
            if (model != null)
            {
                try
                {
                    var modelToAdd = model.GetBaseModel();
                    modelToAdd.LastUpdatedDate = DateTime.Now;
                    modelToAdd.LastUpdatedBy = userID;
                    await _ctx
                        .Set<Model>()
                        .AddAsync(modelToAdd);
                    var success = await _ctx.SaveChangesAsync();
                    var viewModel = new ViewModel();
                    viewModel.SetModelValues(modelToAdd);
                    return (success > 0, viewModel);
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

        public virtual async Task<(bool, ViewModel)> RetrieveByIdAsync(int id)
        {
            var model = await _ctx
                .Set<Model>()
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
                
            var viewModel = new ViewModel();
            bool success = false;
            if (model != null)
            {
                viewModel.SetModelValues(model);
                success = true;
            }
            return (success, viewModel);
        }

        public virtual async Task<IEnumerable<ViewModel>> RetrieveAsync(Expression<Func<Model, bool>> selector)
        {
            var data = await _ctx
                .Set<Model>()
                .Where(selector)
                .ToListAsync();

            var viewModelList = new List<ViewModel>();
            foreach (var model in data)
            {
                var viewModel = new ViewModel();
                viewModel.SetModelValues(model);
                viewModelList.Add(viewModel);
            }
            return viewModelList;
        }

        public virtual async Task<(bool, ViewModel)> UpdateAsync(ViewModel model, string userID)
        {
            if (model != null)
            {
                try
                {
                    var modelToUpdate = 
                        await _ctx
                            .Set<Model>()
                            .Where(w => w.Id == model.Id)
                            .FirstOrDefaultAsync();

                    if (modelToUpdate != null)
                    {
                        modelToUpdate.RestrictedModelUpdate(model.GetBaseModel());
                        modelToUpdate.LastUpdatedDate = DateTime.Now;
                        modelToUpdate.LastUpdatedBy = userID;

                        var success = await _ctx.SaveChangesAsync();
                        var viewModel = new ViewModel();
                        viewModel.SetModelValues(modelToUpdate);
                        return (success > 0, viewModel);
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

        public virtual async Task<bool> DeleteAsync(int id, string UserID)
        {
            if (id > 0)
            {
                try
                {
                    var modelToDelete = 
                        await _ctx
                            .Set<Model>()
                            .Where(w => w.Id == id)
                            .FirstOrDefaultAsync();

                    if (modelToDelete != null)
                    {
                        modelToDelete.DeletionDate = DateTime.Now;
                        modelToDelete.DeletionBy = UserID;
                        var success = await _ctx.SaveChangesAsync();
                        return (success > 0);
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
