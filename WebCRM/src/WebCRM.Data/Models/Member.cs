namespace WebCRM.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for members
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class Member: CRMDataModelBase<Member>
    {
        public string MemberName { get; set; }

        public string UserID { get; set; }

        public override void RestrictedModelUpdate(Member model)
        {
            this.MemberName = model.MemberName;
            this.UserID = model.UserID;
            base.RestrictedModelUpdate(model);
        }
    }
}