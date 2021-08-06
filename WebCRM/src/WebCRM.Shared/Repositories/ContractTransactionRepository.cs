namespace WebCRM.Shared
{
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
    }
}
