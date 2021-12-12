namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Api data controller for account data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class ContractExpenseDataController
        :CRMApiControllerBase<ContractExpense, ContractExpenseViewModel, ICRMRepository<ContractExpense, ContractExpenseViewModel>>
    {
        public ContractExpenseDataController(ICRMRepository<ContractExpense, ContractExpenseViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {

            }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get([FromRoute] int id)
        {
            if (CanViewAll())
            {
                var data = await this._repo.RetrieveAsync(n => n.ContractID == id);
                return Ok(data);
            }
            else
            {
                var restrictedData = await this._repo.RetrieveAsync(RestrictedSelection());
                return Ok(restrictedData.Where(w => w.ContractID == id).ToList());
            }
        }
    }
}