namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Api data controller for account data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class CRMAccountDataController
        :CRMApiControllerBase<CRMAccount, CRMAccountViewModel, ICRMRepository<CRMAccount, CRMAccountViewModel>>
    {
        public CRMAccountDataController(ICRMRepository<CRMAccount, CRMAccountViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {

            }
    }
}