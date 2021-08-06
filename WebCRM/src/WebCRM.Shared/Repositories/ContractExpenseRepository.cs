namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    /// <summary>
    /// CRM repository for contract expense data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    public class ContractExpenseRepository: 
        CRMRepositoryBase<ContractExpense, ContractExpenseViewModel, CRMDBContext>
    {
        public ContractExpenseRepository()
            :base() 
            {
            }

        public ContractExpenseRepository(ILogger logger)
            :base(logger)
            {
            }   
    }
}