namespace WebCRM.Shared
{
    using System.Linq;
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    /// <summary>
    /// CRM repository for contract transaction data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    public class ContractTransactionRepository
        :CRMRepositoryBase<ContractTransaction, ContractTransactionViewModel, CRMDBContext>
    {
        public ContractTransactionRepository(CRMDBContext ctx)
            :base(ctx) 
            {
            }

        public ContractTransactionRepository(ILogger logger, CRMDBContext ctx)
            :base(logger, ctx)
            {
            }

        public override (bool, ContractTransactionViewModel) Create(ContractTransactionViewModel model, string userID)
        {
            var (success, viewModel) = base.Create(model, userID);
            if (success && viewModel != null && viewModel.ContractID != 0)
            {
                var contractToUpdate = this._ctx
                    .Contracts
                    .Where(w => w.Id == viewModel.ContractID)
                    .FirstOrDefault();
                if (contractToUpdate != null)
                {
                    if (!contractToUpdate.LastPaymentRecievedDate.HasValue
                        || viewModel.TransactionDate > contractToUpdate.LastPaymentRecievedDate.Value)
                    {
                        contractToUpdate.LastPaymentRecievedDate = viewModel.TransactionDate;
                    }
                    contractToUpdate.TotalPaidAmount += viewModel.TransactionAmount;
                    contractToUpdate.LastUpdatedBy = userID;
                    contractToUpdate.LastUpdatedDate = System.DateTime.Now;
                    this._ctx.Contracts.Update(contractToUpdate);
                    var updateCt = this._ctx.SaveChanges();
                    if (updateCt == 0)
                    {
                        viewModel.ValidationErrorMessages.Add("Unable to update Contract");
                    }
                }
                else
                {
                    viewModel.ValidationErrorMessages.Add("Cannot find associated contract");
                }
            }
            return (success, viewModel);
        }
    }
}
