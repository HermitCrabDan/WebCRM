namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Api data controller for account data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("[controller]")]
    public class ContractExpenseDataController
        :CRMApiControllerBase<ContractExpense, ContractExpenseViewModel, ICRMRepository<ContractExpense, ContractExpenseViewModel>>
    {
        public AccountDataController(ICRMRepository<ContractExpense, ContractExpenseViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {

            }
    }
}