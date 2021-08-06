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
        string UserID { get; }

        bool IsAdmin { get; }

        bool IsMember { get; }

        bool CanViewReports { get; }

        bool CanManageUsers { get; }

        bool CanViewAll(Type T);

        bool CanCreate(Type T);

        bool CanUpdate(Type T);

        bool CanDelete(Type T);
    }
}