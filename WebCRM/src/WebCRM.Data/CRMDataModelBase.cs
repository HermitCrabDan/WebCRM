namespace  WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class CRMDataModelBase<T>: ICRMDataModel<T>
        where T: ICRMDataModel<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public virtual void RestrictedModelUpdate(T model)
        {
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
        }
    }
}