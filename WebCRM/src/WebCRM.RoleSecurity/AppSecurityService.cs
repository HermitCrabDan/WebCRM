namespace WebCRM.RoleSecurity
{
    using System;
    using System.Linq;
    using WebCRM.Data;
    using System.Collections.Generic;
    
    ///<summary>
    ///Implementation of CRM security service
    ///</summary>
    ///<author>Daniel Lee Graf</author>
    public class AppSecurityService: IAppSecurityService
    {
        private List<Type> UpdateTypeList = new List<Type>();
        private List<Type> CreateTypeList = new List<Type>();
        private List<Type> DeleteTypeList = new List<Type>();
        private List<Type> AllViewableTypeList = new List<Type>();

        public AppSecurityService() 
        {
            this.IsAdmin = true;
        }

        public string UserID { get; private set; }

        public bool IsMember { get; set; }

        public bool IsAdmin { get; private set; }

        public bool CanViewReports { get; private set; }

        public bool CanManageUsers { get; private set; }

        public bool CanViewAll(Type T)
        {
            return IsAdmin || this.AllViewableTypeList.Contains(T);
        }

        public bool CanCreate(Type T)
        {
            return IsAdmin || this.CreateTypeList.Contains(T);
        }

        public bool CanUpdate(Type T)
        {
            return IsAdmin || this.UpdateTypeList.Contains(T);
        }

        public bool CanDelete(Type T)
        {
            return IsAdmin || this.DeleteTypeList.Contains(T);
        }
    }
}
