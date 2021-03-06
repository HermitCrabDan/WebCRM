namespace WebCRM.RoleSecurity
{
    using System;
    using System.Collections.Generic;

    ///<summary>
    ///CRM Security Interface
    ///</summary>
    ///<author>Daniel Lee Graf</author>
    public interface IAppSecurityService
    {
        string UserID { get; }

        bool IsAdmin { get; }

        bool IsMember { get; }

        int MemberId { get; }

        bool CanAccountEnterData { get; }

        bool CanViewReports { get; }

        bool CanManageUsers { get; }
    }
}