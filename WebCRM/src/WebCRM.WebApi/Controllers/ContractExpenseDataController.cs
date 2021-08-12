namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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

        protected override bool CanDelete()
        {
            return false;
        }

        [HttpPost]
        public override IActionResult Create([FromBody] ContractExpenseViewModel model)
        {
            model.ExpenseEnteredBy = this._security.UserID;
            model.ExpenseEnteredDate = System.DateTime.Now;
            return base.Create(model);
        }

        [HttpPut]
        public override IActionResult Update([FromBody] ContractExpenseViewModel model)
        {
            if (model.ExpenseCancelDate.HasValue && string.IsNullOrWhiteSpace(model.ExpenseCanceledBy))
            {
                model.ExpenseCanceledBy = this._security.UserID;
            }    
            return base.Update(model);
        }
    }
}