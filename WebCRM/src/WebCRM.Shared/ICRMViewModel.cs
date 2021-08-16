namespace WebCRM.Shared
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Base ViewModel interface to implete the IsValid and GetBaseModel methods
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public interface ICRMViewModel<Model>
    {
        int Id { get; set; }

        DateTime CreationDate { get; set; }

        string CreatedBy { get; set; }

        DateTime? LastUpdatedDate { get; set; }

        string LastUpdatedBy { get; set; }

        DateTime? DeletionDate { get; set; }

        string DeletionBy { get; set; }

        List<string> ValidationErrorMessages { get; set; }
        
        void SetModelValues(Model model);

        bool IsValid();

        Model GetBaseModel();
    }
}