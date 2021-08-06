namespace WebCRM.RoleSecurity
{
    using System;
    using System.Collections.Generic;

    ///<summary>
    ///CRM Security Interface
    ///</summary>
    ///<author>Daniel L. Graf</author>
    public interface IAppSecurityService
    {
        int UserID { get; }

        bool IsAdmin { get; }

        bool IsMember { get; }

        bool CanAccountEnterData { get; }

        bool CanViewReports { get; }

        bool CanManageUsers { get; }
    }
}