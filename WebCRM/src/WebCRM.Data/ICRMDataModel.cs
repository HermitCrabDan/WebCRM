namespace WebCRM.Data
{
    using System;

    /// <summary>
    /// Interface to ensure base functionality of models
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public interface ICRMDataModel<DataModel>
    {
        int Id { get; set; }

        DateTime CreationDate { get; set; }

        string CreatedBy { get; set; }

        DateTime? LastUpdatedDate { get; set; }

        string LastUpdatedBy { get; set; }

        DateTime? DeletionDate { get; set; }

        string DeletionBy { get; set; }

        void RestrictedModelUpdate(DataModel model);
    }
}