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

        protected override bool CanDelete()
        {
            return false;
        }

        public override IActionResult Create([FromBody] ContractTransactionViewModel model)
        {
            model.TransactionEnteredBy = this._security.UserID;
            model.TransactionEnteredDate = System.DateTime.Now;
            return base.Create(model);
        }

        public override IActionResult Update([FromBody] ContractTransactionViewModel model)
        {
            if (model.TransactionCancelDate.HasValue && string.IsNullOrWhiteSpace(model.TransactionCanceledBy))
            {
                model.TransactionCanceledBy = this._security.UserID;
            }
            return base.Update(model);
        }
    }
}