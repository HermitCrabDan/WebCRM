namespace WebCRM.Data.Models
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }
    }
}
