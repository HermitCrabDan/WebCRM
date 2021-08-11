namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.RoleSecurity;
    using WebCRM.Shared;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;

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
    }
}