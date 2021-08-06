namespace WebCRM.Shared
{
    using System;
    using System.Linq;
    using WebCRM.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        where Context: DbContext,new()
    {
        private ILogger _logger;

        private Context ctx;

        public CRMRepositoryBase() {}

        public CRMRepositoryBase(ILogger logger)
        {
            _logger = logger;
        }

        public virtual (bool, ViewModel) Create(ViewModel model)
        {
            if (model != null)
            {
                try
                {
                    using (ctx = new Context())
                    {
                        var modelToAdd = model.GetBaseModel();
                        ctx
                            .Set<Model>()
                            .Add(modelToAdd);
                        var success = (ctx.SaveChanges() > 0);
                        var viewModel = new ViewModel();
                        viewModel.SetModelValues(modelToAdd);
                        return (success, viewModel);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to create {typeof(Model).Name}");
                    }
                }
            }
            return (false, model);
        }

        public virtual async Task<(bool, ViewModel)> CreateAsync(ViewModel model)
        {
            if (model != null)
            {
                try
                {
                    using (ctx = new Context())
                    {
                        var modelToAdd = model.GetBaseModel();
                        await ctx
                            .Set<Model>()
                            .AddAsync(modelToAdd);
                        var savedRecords = await ctx.SaveChangesAsync();
                        var success = (savedRecords > 0);
                        var viewModel = new ViewModel();
                        viewModel.SetModelValues(modelToAdd);
                        return (success, viewModel);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to create {typeof(Model).Name}");
                    }
                }
            }
            return (false, model);
        }

        public virtual (bool, ViewModel) RetrieveById(int id)
        {
            using (var ctx = new CRMDBContext())
            {
                var model = ctx
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
        }

        public virtual IEnumerable<ViewModel> Retrieve(Func<Model, bool> selector)
        {
            using (var ctx = new CRMDBContext())
            {
                var data = ctx
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
        }

        public virtual (bool, ViewModel) Update(ViewModel model)
        {
            if (model != null)
            {
                try
                {
                    using (var ctx = new CRMDBContext())
                    {
                        var modelToUpdate = 
                            ctx
                                .Set<Model>()
                                .Where(w => w.Id == model.Id)
                                .FirstOrDefault();
                        if (modelToUpdate != null)
                        {
                            modelToUpdate.RestrictedModelUpdate(model.GetBaseModel());
                            ctx.Update(modelToUpdate);
                            var success = ctx.SaveChanges() > 0;
                            var viewModel = new ViewModel();
                            viewModel.SetModelValues(modelToUpdate);
                            return (success, viewModel);
                        }
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

        public virtual async Task<(bool, ViewModel)> UpdateAsync(ViewModel model)
        {
            if (model != null)
            {
                try
                {
                    using (var ctx = new CRMDBContext())
                    {
                        var modelToUpdate = 
                            ctx
                                .Set<Model>()
                                .Where(w => w.Id == model.Id)
                                .FirstOrDefault();
                        if (modelToUpdate != null)
                        {
                            modelToUpdate.RestrictedModelUpdate(model.GetBaseModel());
                            ctx.Update(modelToUpdate);
                            var savedRecords = await ctx.SaveChangesAsync();

                            var success = savedRecords > 0;
                            var viewModel = new ViewModel();
                            viewModel.SetModelValues(modelToUpdate);
                            return (success, viewModel);
                        }
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

        public virtual bool Delete(ViewModel model)
        {
            if (model != null)
            {
                try
                {
                    using (var ctx = new CRMDBContext())
                    {
                        var modelToDelete = 
                            ctx
                                .Set<Model>()
                                .Where(w => w.Id == model.Id)
                                .FirstOrDefault();
                        if (modelToDelete != null)
                        {
                            ctx
                                .Set<Model>()
                                .Remove(modelToDelete);
                            return (ctx.SaveChanges() > 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to delete {typeof(Model).Name}, Id:{model?.Id}");
                    }
                }
            }
            return false;
        }

        public virtual async Task<bool> DeleteAsync(ViewModel model)
        {
            if (model != null)
            {
                try
                {
                    using (var ctx = new CRMDBContext())
                    {
                        var modelToDelete = 
                            ctx
                                .Set<Model>()
                                .Where(w => w.Id == model.Id)
                                .FirstOrDefault();
                        if (modelToDelete != null)
                        {
                            ctx
                                .Set<Model>()
                                .Remove(modelToDelete);
                            var deletedRecords = await ctx.SaveChangesAsync();
                            return (deletedRecords > 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to delete {typeof(Model).Name}, Id:{model?.Id}");
                    }
                }
            }
            return false;
        }

    }
}
