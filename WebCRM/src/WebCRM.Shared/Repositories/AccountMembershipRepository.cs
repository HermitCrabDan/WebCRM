namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// CRM Repository for account membership data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class AccountMembershipRepository
        :CRMRepositoryBase<AccountMembership, AccountMembershipViewModel, CRMDBContext>
    {
        public AccountMembershipRepository(CRMDBContext context)
            :base(context)
            {
            }

        public AccountMembershipRepository(ILogger logger, CRMDBContext context)
            :base(logger, context)
            {
            }
    }
}