namespace WebCRM.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
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
        Task<(bool, ViewModel)> CreateAsync(ViewModel model, string userID);

        Task<IEnumerable<ViewModel>> RetrieveAsync(Expression<Func<Model, bool>> selector);

        Task<(bool, ViewModel)> RetrieveByIdAsync(int id);

        Task<(bool, ViewModel)> UpdateAsync(ViewModel modelToUpdate, string userID);

        Task<bool> DeleteAsync(int id, string userID);
    }
}