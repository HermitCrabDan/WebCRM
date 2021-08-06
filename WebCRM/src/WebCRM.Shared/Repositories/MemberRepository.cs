namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    /// <summary>
    /// CRM repository for member data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class MemberRepository: CRMRepositoryBase<Member, MemberViewModel, CRMDBContext>
    {
        public MemberRepository(CRMDBContext ctx)
            :base(ctx)
            {
            }
            
        public MemberRepository(ILogger logger, CRMDBContext ctx)
            :base(logger, ctx)
            {
            }
    }
}