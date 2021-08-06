namespace WebCRM.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebCRM.Data;

    /// <summary>
    /// Create, Read, Update, and Delete implementations for basic CRM repositories
    /// </summary>
    /// <typeparam name="Model">The data model</typeparam>
    /// <typeparam name="ViewModel">the view model</typeparam>
    /// <author>Daniel Lee Graf</author>
    public interface ICRMRepository<Model, ViewModel> 
        where Model:ICRMDataModel<Model>
        where ViewModel:ICRMViewModel<Model>
    {
        (bool, ViewModel) Create(ViewModel model);

        Task<(bool, ViewModel)> CreateAsync(ViewModel model);

        IEnumerable<ViewModel> Retrieve(Func<Model, bool> selector);

        (bool, ViewModel) RetrieveById(int id);

        (bool, ViewModel) Update(ViewModel modelToUpdate);

        Task<(bool, ViewModel)> UpdateAsync(ViewModel modelToUpdate);

        bool Delete(int id);

        Task<bool> DeleteAsync(int id);
    }
}