namespace WebCRM.Shared
{
    using System.Collections.Generic;
    /// <summary>
    /// Base ViewModel interface to implete the IsValid and GetBaseModel methods
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public interface ICRMViewModel<Model>
    {
        int Id { get; set; }

        List<string> ValidationErrorMessages { get; set; }
        
        void SetModelValues(Model model);

        bool IsValid();

        Model GetBaseModel();
    }
}