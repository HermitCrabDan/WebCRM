namespace WebCRM.RoleSecurity
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    ///<summary>
    ///Implementation of CRM security service
    ///</summary>
    ///<author>Daniel Lee Graf</author>
    public class AppSecurityService: IAppSecurityService
    {
        public AppSecurityService() 
        {
            this.IsAdmin = true;
            this.UserID = "admin";
        }

        public string UserID { get; private set; }

        public bool IsMember { get; private set; }

        public int MemberId { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool CanAccountEnterData { get; private set; }

        public bool CanViewReports { get; private set; }

        public bool CanManageUsers { get; private set; }
    }
}
