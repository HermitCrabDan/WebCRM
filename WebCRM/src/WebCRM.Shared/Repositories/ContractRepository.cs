namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    /// <summary>
    /// CRM repository for contract data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class ContractRepository: CRMRepositoryBase<Contract, ContractViewModel, CRMDBContext>
    {
        public ContractRepository()
            :base()
            {
            }

        public ContractRepository(ILogger logger)
            :base(logger)
            {
            }
    }
}