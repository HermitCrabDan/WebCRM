namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using System.Linq;
    using WebCRM.RoleSecurity;
    using WebCRM.Shared;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    /// <summary>
    /// CRM Api controller for Account Membership Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountMembershipDataController
        :CRMApiControllerBase<AccountMembership, AccountMembershipViewModel, ICRMRepository<AccountMembership,AccountMembershipViewModel>>
    {
        public AccountMembershipDataController(ICRMRepository<AccountMembership, AccountMembershipViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {
                
            }

        protected override Expression<Func<AccountMembership, bool>> RestrictedSelection()
        {
            if (this._security.IsMember)
            {
                return n => n.MemberID == this._security.MemberId;
            }
            return base.RestrictedSelection();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get([FromRoute] int id)
        {
            if (CanViewAll())
            {
                var data = await this._repo.RetrieveAsync(n => n.AccountID == id);
                return Ok(data);
            }
            else
            {
                var restrictedData = await this
                    ._repo.RetrieveAsync(RestrictedSelection());

                return Ok(restrictedData
                    .Where(w => w.AccountID == id)
                    .ToList());
            }
        }
    }
}