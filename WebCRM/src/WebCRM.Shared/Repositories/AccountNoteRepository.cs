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
        public AccountNoteRepository()
            :base()
            {
            }

        public AccountNoteRepository(ILogger logger)
            :base(logger)
            {
            }
    }
}
