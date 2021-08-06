namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    /// <summary>
    /// CRM repository for account note data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class AccountNoteRepository: CRMRepositoryBase<AccountNote, AccountNoteViewModel, CRMDBContext>
    {
        public AccountNoteRepository(CRMDBContext context)
            :base(context)
            {
            }

        public AccountNoteRepository(ILogger logger, CRMDBContext context)
            :base(logger, context)
            {
            }
    }
}
