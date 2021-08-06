namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    /// <summary>
    /// CRM repository for member testimonial data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    public class MemberTestimonialRepository: 
        CRMRepositoryBase<MemberTestimonial, MemberTestimonialViewModel, CRMDBContext>
    {
        public MemberTestimonialRepository(CRMDBContext ctx)
            :base(ctx)
            {
            }
            
        public MemberTestimonialRepository(ILogger logger, CRMDBContext ctx)
            :base(logger, ctx)
            {
            }
    }
}