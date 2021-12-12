namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    /// <summary>
    /// CRM Api controller for member data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberDataController
        :CRMApiControllerBase<Member,MemberViewModel, ICRMRepository<Member, MemberViewModel>>
    {
        public MemberDataController(ICRMRepository<Member, MemberViewModel> repo, IAppSecurityService security)
            :base(repo, security)
        {

        }

        protected override Expression<Func<Member, bool>> RestrictedSelection()
        {
            if (this._security.IsMember)
            {
                return n => n.Id == this._security.MemberId;
            }
            return base.RestrictedSelection();
        }
    }
}
