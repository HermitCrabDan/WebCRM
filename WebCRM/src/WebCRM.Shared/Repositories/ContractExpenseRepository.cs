namespace WebCRM.Shared
{
    using System;
    using System.Linq;
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CRM repository for contract expense data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    public class ContractExpenseRepository: 
        CRMRepositoryBase<ContractExpense, ContractExpenseViewModel, CRMDBContext>
    {
        public ContractExpenseRepository(CRMDBContext ctx)
            :base(ctx) 
            {
            }

        public ContractExpenseRepository(ILogger logger, CRMDBContext ctx)
            :base(logger, ctx)
            {
            }  

        public override async Task<(bool, ContractExpenseViewModel)> CreateAsync(ContractExpenseViewModel model, string userID)
        {
            var (success, viewModel) = await base.CreateAsync(model, userID);
            if (success && viewModel != null && viewModel.ContractID != 0)
            {
                var contractToUpdate = await this._ctx
                    .Contracts
                    .Where(w => w.Id == viewModel.ContractID)
                    .FirstOrDefaultAsync();

                if (contractToUpdate != null)
                {
                    if (!contractToUpdate.LastExpensePaymentDate.HasValue
                        || viewModel.ExpenseDate > contractToUpdate.LastExpensePaymentDate.Value)
                    {
                        contractToUpdate.LastExpensePaymentDate = viewModel.ExpenseDate;
                    }
                    contractToUpdate.TotalExpenseAmount += viewModel.ExpenseAmount;
                    contractToUpdate.LastUpdatedBy = userID;
                    contractToUpdate.LastUpdatedDate = System.DateTime.Now;
                    this._ctx.Contracts.Update(contractToUpdate);
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

        public override async Task<(bool, ContractExpenseViewModel)> UpdateAsync(ContractExpenseViewModel model, string userID)
        {
            if (model != null)
            {
                try
                {
                    var modelToUpdate = 
                        await this._ctx
                            .ContractExpenses
                            .Where(w => w.Id == model.Id)
                            .FirstOrDefaultAsync();

                    if (modelToUpdate != null)
                    {
                        var amountDiff = model.ExpenseAmount - modelToUpdate.ExpenseAmount;
                        if (modelToUpdate.DeletionDate.HasValue && !model.DeletionDate.HasValue)
                        {
                            amountDiff = model.ExpenseAmount;
                        }
                        modelToUpdate.RestrictedModelUpdate(model.GetBaseModel());
                        modelToUpdate.LastUpdatedDate = DateTime.Now;
                        modelToUpdate.LastUpdatedBy = userID;

                        if (amountDiff != 0)
                        {
                            var contractToUpdate = await this._ctx
                                .Contracts
                                .Where(w => w.Id == model.ContractID)
                                .FirstOrDefaultAsync();

                            if (contractToUpdate != null)
                            {
                                if (!contractToUpdate.LastExpensePaymentDate.HasValue
                                    || model.ExpenseDate > contractToUpdate.LastExpensePaymentDate.Value)
                                {
                                    contractToUpdate.LastExpensePaymentDate = model.ExpenseDate;
                                }
                                contractToUpdate.TotalExpenseAmount += amountDiff;
                                contractToUpdate.LastUpdatedBy = userID;
                                contractToUpdate.LastUpdatedDate = System.DateTime.Now;
                            }
                        }
                        var success = await _ctx.SaveChangesAsync();
                        var viewModel = new ContractExpenseViewModel();
                        viewModel.SetModelValues(modelToUpdate);
                        return (success > 0, viewModel);
                    }
                }
                catch (Exception ex) 
                { 
                    if (_logger != null)
                    {
                        _logger.LogError(ex, $"Failed to update Contract Expense, Id:{model?.Id}");
                    }
                }
            }
            return (false, model);
        }

        public override async Task<bool> DeleteAsync(int id, string UserID)
        {
            var modelToDelete = await this._ctx.ContractExpenses.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (modelToDelete != null)
            {
                try
                {
                    modelToDelete.DeletionDate = DateTime.Now;
                    modelToDelete.DeletionBy = UserID;

                    if (modelToDelete.ExpenseAmount != 0)
                    {
                        var contractToUpdate = await this._ctx.Contracts.Where(w => w.Id == modelToDelete.ContractID).FirstOrDefaultAsync();

                        if (contractToUpdate != null)
                        {
                            contractToUpdate.TotalPaidAmount -= modelToDelete.ExpenseAmount;
                            if (contractToUpdate.LastExpensePaymentDate.HasValue
                                && contractToUpdate.LastExpensePaymentDate.Value == modelToDelete.ExpenseDate)
                            {
                                contractToUpdate.LastExpensePaymentDate = null;
                            }
                        }
                    }

                    var success = await this._ctx.SaveChangesAsync();
                    return success > 0;
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        this._logger.LogError(ex, $"Failed to delete Contract Expense, Id:{ id }");
                    }
                }
            }
            return false;
        }
    }
}