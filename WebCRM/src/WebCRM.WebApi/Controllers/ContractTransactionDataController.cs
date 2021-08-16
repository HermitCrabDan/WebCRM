namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    /// CRM Api controller for Contract Transactions
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    [ApiController]
    [Route("api/[controller]")]
    public class ContractTransactionDataController
        :CRMApiControllerBase<ContractTransaction, ContractTransactionViewModel, ICRMRepository<ContractTransaction, ContractTransactionViewModel>>
    {
        public ContractTransactionDataController(ICRMRepository<ContractTransaction, ContractTransactionViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {
                
            }
    }
}