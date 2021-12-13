namespace WebCRM.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Stored user data
    /// </summary>
    /// <Author>Daniel Lee Graf</Author>
    public class CRMUser: CRMDataModelBase<CRMUser>
    {
        public CRMUser()
        {
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public override void RestrictedModelUpdate(CRMUser model)
        {
            base.RestrictedModelUpdate(model);
            this.Username = model.Username;
            this.Password = model.Password;
            this.EmailAddress = model.EmailAddress;
        }
    }
}
