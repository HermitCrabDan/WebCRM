namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
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
        
        [HttpGet("{id}")]
        public override async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id == default)
            {
                return BadRequest();
            }

            if (CanViewAll())
            {
                var data = await this._repo.RetrieveAsync(n => n.ContractID == id);
                return Ok(data);
            }

            var viewableData = await this._repo.RetrieveAsync(RestrictedSelection());
            var selectedData = viewableData.Where(w => w.ContractID == id).ToList();

            if (selectedData != null)
            {
                return Ok(selectedData);
            }

            return NotFound();
        }
    }
}