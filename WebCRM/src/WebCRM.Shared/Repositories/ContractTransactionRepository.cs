namespace WebCRM.Shared
{
    using System;
    using System.Linq;
    using WebCRM.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

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

        public override async Task<(bool, ContractTransactionViewModel)> CreateAsync(ContractTransactionViewModel model, string userID)
        {
            var (success, viewModel) = await base.CreateAsync(model, userID);
            if (success && viewModel != null && viewModel.ContractID != 0 && model.TransactionAmount != 0)
            {
                var contractToUpdate = await this._ctx
                    .Contracts
                    .Where(w => w.Id == viewModel.ContractID)
                    .FirstOrDefaultAsync();

                if (contractToUpdate != null)
                {
                    var paymentDate = new DateTime(
                            model.PaymentYear, 
                            model.PaymentMonth, 
                            contractToUpdate.PaymentDate
                        );
                    if (!contractToUpdate.LastPaymentRecievedDate.HasValue
                        || paymentDate > contractToUpdate.LastPaymentRecievedDate.Value)
                    {
                        contractToUpdate.LastPaymentRecievedDate = paymentDate;
                    }
                    contractToUpdate.TotalPaidAmount += viewModel.TransactionAmount;
                    contractToUpdate.LastUpdatedBy = userID;
                    contractToUpdate.LastUpdatedDate = System.DateTime.Now;
                    
                    var updateCt = await this._ctx.SaveChangesAsync();
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

        public override async Task<(bool, ContractTransactionViewModel)> UpdateAsync(ContractTransactionViewModel model, string userID)
        {
            if (model != null)
            {
                try
                {
                    var modelToUpdate = 
                        await this._ctx
                            .ContractTransactions
                            .Where(w => w.Id == model.Id)
                            .FirstOrDefaultAsync();

                    if (modelToUpdate != null)
                    {
                        var amountDiff = model.TransactionAmount - modelToUpdate.TransactionAmount;
                        if (modelToUpdate.DeletionDate.HasValue && !model.DeletionDate.HasValue)
                        {
                            amountDiff = model.TransactionAmount;
                        }
                        modelToUpdate.RestrictedModelUpdate(model.GetBaseModel());
                        modelToUpdate.LastUpdatedDate = DateTime.Now;
                        modelToUpdate.LastUpdatedBy = userID;

                        var success = await _ctx.SaveChangesAsync();
                        
                        if (amountDiff != 0)
                        {
                            var contractToUpdate = await this._ctx
                                .Contracts
                                .Where(w => w.Id == model.ContractID)
                                .FirstOrDefaultAsync();

                            if (contractToUpdate != null)
                            {
                                contractToUpdate.TotalPaidAmount += amountDiff;

                                var paymentDate = new DateTime(
                                        model.PaymentYear, 
                                        model.PaymentMonth, 
                                        contractToUpdate.PaymentDate
                                    );
                                if (!contractToUpdate.LastPaymentRecievedDate.HasValue
                                    || paymentDate > contractToUpdate.LastPaymentRecievedDate.Value)
                                    {
                                        contractToUpdate.LastPaymentRecievedDate = paymentDate;
                                    }
                                contractToUpdate.LastUpdatedBy = userID;
                                contractToUpdate.LastUpdatedDate = System.DateTime.Now;
                                success += await _ctx.SaveChangesAsync();
                            }
                        }
                        var viewModel = new ContractTransactionViewModel();
                        viewModel.SetModelValues(modelToUpdate);
                        return (success > 0, viewModel);
                    }
                }
                catch (Exception ex) 
                { 
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to update Contract Transaction, Id:{model?.Id}");
                    }
                }
            }
            return (false, model);
        }

        public override async Task<bool> DeleteAsync(int id, string userID)
        {
            var modelToDelete = await this._ctx.ContractTransactions.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (modelToDelete != null)
            {
                try
                {
                    modelToDelete.DeletionDate = DateTime.Now;
                    modelToDelete.DeletionBy = userID;
                    var success = await this._ctx.SaveChangesAsync();

                    if (modelToDelete.TransactionAmount != 0)
                    {
                        var contractToUpdate = await this._ctx.Contracts.Where(w => w.Id == modelToDelete.ContractID).FirstOrDefaultAsync();

                        if (contractToUpdate != null)
                        {
                            contractToUpdate.TotalPaidAmount -= modelToDelete.TransactionAmount;
                            var paymentDate = new DateTime(
                                    modelToDelete.PaymentYear,
                                    modelToDelete.PaymentMonth,
                                    contractToUpdate.PaymentDate
                                );
                            if (contractToUpdate.LastPaymentRecievedDate.HasValue
                                && contractToUpdate.LastPaymentRecievedDate.Value == paymentDate)
                            {
                                var firstPaymentDate = new DateTime(
                                        contractToUpdate.ContractStartDate.Year,
                                        contractToUpdate.ContractStartDate.Month,
                                        contractToUpdate.PaymentDate
                                    );
                                if (contractToUpdate.ContractStartDate.Day > contractToUpdate.PaymentDate)
                                {
                                    firstPaymentDate.AddMonths(1);
                                }
                                contractToUpdate.LastPaymentRecievedDate = 
                                    (firstPaymentDate <= paymentDate.AddMonths(-1))? paymentDate.AddMonths(-1) : null;
                            }
                            contractToUpdate.LastUpdatedBy = userID;
                            contractToUpdate.LastUpdatedDate = System.DateTime.Now;
                            success += await this._ctx.SaveChangesAsync();
                        }
                    }

                    return success > 0;
                }
                catch (Exception ex)
                {
                    if (this._logger != null)
                    {
                        this._logger.LogError(ex, $"Failed to delete Contract Transaction, Id:{ id }");
                    }
                }
            }
            return false;
        }
    }
}
