namespace WebCRM.Shared
{
    using System;
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
            if (success && viewModel != null && viewModel.ContractID != 0 && !model.IsFee)
            {
                var contractToUpdate = this._ctx
                    .Contracts
                    .Where(w => w.Id == viewModel.ContractID)
                    .FirstOrDefault();
                if (contractToUpdate != null)
                {
                    var paymentDate = new DateTime(model.PaymentYear, model.PaymentMonth, contractToUpdate.PaymentDate);
                    if (!contractToUpdate.LastPaymentRecievedDate.HasValue
                        || paymentDate > contractToUpdate.LastPaymentRecievedDate.Value)
                    {
                        contractToUpdate.LastPaymentRecievedDate = paymentDate;
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

        public override (bool, ContractTransactionViewModel) Update(ContractTransactionViewModel model, string userID)
        {
            if (model != null)
            {
                try
                {
                    var modelToUpdate = 
                        this._ctx
                            .ContractTransactions
                            .Where(w => w.Id == model.Id)
                            .FirstOrDefault();
                    if (modelToUpdate != null)
                    {
                        var amountDiff = model.TransactionAmount - modelToUpdate.TransactionAmount;
                        if (modelToUpdate.DeletionDate.HasValue && !model.DeletionDate.HasValue)
                        {
                            amountDiff = model.TransactionAmount;
                        }
                        else if (modelToUpdate.IsFee != model.IsFee)
                        {
                            amountDiff = (model.IsFee) ? -1 * modelToUpdate.TransactionAmount : model.TransactionAmount;
                        }
                        modelToUpdate.RestrictedModelUpdate(model.GetBaseModel());
                        modelToUpdate.LastUpdatedDate = DateTime.Now;
                        modelToUpdate.LastUpdatedBy = userID;

                        this._ctx.ContractTransactions.Update(modelToUpdate);
                        
                        if (amountDiff != 0)
                        {
                            var contractToUpdate = this._ctx
                                .Contracts
                                .Where(w => w.Id == model.ContractID)
                                .FirstOrDefault();
                            if (contractToUpdate != null)
                            {
                                contractToUpdate.TotalPaidAmount += amountDiff;

                                var paymentDate = new DateTime(
                                    (modelToUpdate.PaymentYear > 0) ? modelToUpdate.PaymentYear : modelToUpdate.TransactionDate.Year,
                                    (modelToUpdate.PaymentMonth > 0) ? modelToUpdate.PaymentMonth : modelToUpdate.TransactionDate.Month,
                                    contractToUpdate.PaymentDate);
                                if (!contractToUpdate.LastPaymentRecievedDate.HasValue
                                    || paymentDate > contractToUpdate.LastPaymentRecievedDate.Value)
                                    {
                                        contractToUpdate.LastPaymentRecievedDate = paymentDate;
                                    }
                                this._ctx.Contracts.Update(contractToUpdate);
                            }
                        }
                        var success = _ctx.SaveChanges() > 0;
                        var viewModel = new ContractTransactionViewModel();
                        viewModel.SetModelValues(modelToUpdate);
                        return (success, viewModel);
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

        public override bool Delete(int id, string UserID)
        {
            var modelToDelete = this._ctx.ContractTransactions.Where(w => w.Id == id).FirstOrDefault();
            if (modelToDelete != null)
            {
                try
                {
                    modelToDelete.DeletionDate = DateTime.Now;
                    modelToDelete.DeletionBy = UserID;
                    this._ctx.ContractTransactions.Update(modelToDelete);

                    if (modelToDelete.TransactionAmount != 0 && !modelToDelete.IsFee)
                    {
                        var contractToUpdate = this._ctx.Contracts.Where(w => w.Id == modelToDelete.ContractID).FirstOrDefault();
                        if (contractToUpdate != null)
                        {
                            contractToUpdate.TotalPaidAmount -= modelToDelete.TransactionAmount;
                            var paymentDate = new DateTime(
                                (modelToDelete.PaymentYear > 0)? modelToDelete.PaymentYear:modelToDelete.TransactionDate.Year,
                                (modelToDelete.PaymentMonth > 0)? modelToDelete.PaymentMonth:modelToDelete.TransactionDate.Month,
                                contractToUpdate.PaymentDate);
                            if (contractToUpdate.LastPaymentRecievedDate.HasValue
                                && contractToUpdate.LastPaymentRecievedDate.Value == paymentDate)
                            {
                                contractToUpdate.LastPaymentRecievedDate = null;
                            }
                            this._ctx.Contracts.Update(contractToUpdate);
                        }
                    }

                    return this._ctx.SaveChanges() > 0;
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
