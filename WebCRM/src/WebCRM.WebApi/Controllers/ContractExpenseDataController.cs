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
        public override IActionResult Get([FromRoute] int id)
        {
            if (CanViewAll())
            {
                return Ok(this._repo.Retrieve(n => n.ContractID == id));
            }
            else
            {
                var data = this._repo.Retrieve(RestrictedSelection());
                return Ok(data.Where(w => w.ContractID == id).ToList());
            }
        }
    }
}