namespace WebCRM.Shared
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using WebCRM.Data;
    using WebCRM.Helpers;

    /// <summary>
    /// Base class for ICRMViewModels
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public abstract class CRMViewModelBase<Model>:ICRMViewModel<Model>
        where Model:ICRMDataModel<Model>,new()
    {
        public CRMViewModelBase()
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public int Id { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedDateString { get; set; }

        public string DeletionDateString { get; set; }

        public List<string> ValidationErrorMessages { get; set; }

        public virtual void SetModelValues(Model model)
        {
            this.Id = model.Id;
            this.LastUpdatedBy = XSSFilterHelper.FilterForXSS(model.LastUpdatedBy);
            this.LastUpdatedDate = model.LastUpdatedDate;
            this.DeletionDate = model.DeletionDate;
            this.DeletionBy = XSSFilterHelper.FilterForXSS(model.DeletionBy);

            this.LastUpdatedDateString = String.Format("{0:MM-dd-yyyy}", model.LastUpdatedDate);
            this.DeletionDateString = String.Format("{0:MM-dd-yyyy}", model.DeletionDate);
        }

        public virtual bool IsValid()
        {
            return false;
        }

        public virtual Model GetBaseModel()
        {
            return new Model
            {
                Id = this.Id,
                DeletionBy = this.DeletionBy,
                DeletionDate = this.DeletionDate,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate
            };
        }
    }
}