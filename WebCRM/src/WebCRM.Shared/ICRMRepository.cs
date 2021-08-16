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
        (bool, ViewModel) Create(ViewModel model, string userID);

        Task<(bool, ViewModel)> CreateAsync(ViewModel model, string userID);

        IEnumerable<ViewModel> Retrieve(Func<Model, bool> selector);

        Task<IEnumerable<ViewModel>> RetrieveAsync(Func<Model, bool> selector);

        (bool, ViewModel) RetrieveById(int id);

        Task<(bool, ViewModel)> RetrieveByIdAsync(int id);

        (bool, ViewModel) Update(ViewModel modelToUpdate, string userID);

        Task<(bool, ViewModel)> UpdateAsync(ViewModel modelToUpdate, string userID);

        bool Delete(int id, string userID);

        Task<bool> DeleteAsync(int id, string userID);
    }
}