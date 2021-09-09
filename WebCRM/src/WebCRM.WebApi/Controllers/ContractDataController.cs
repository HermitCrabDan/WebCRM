namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;

    /// <summary>
    /// CRM Api controller for Contract Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    [ApiController]
    [Route("api/[controller]")]
    public class ContractDataController
        :CRMApiControllerBase<Contract, ContractViewModel, ICRMRepository<Contract, ContractViewModel>>
    {
        public ContractDataController(ICRMRepository<Contract, ContractViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {
            }
    }
}