namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    /// <summary>
    /// CRM Api controller for Member Testimonial Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("[controller]")]
    public class MemberTestimonialDataController
        :CRMApiControllerBase<MemberTestimonial,MemberTestimonialViewModel, ICRMRepository<MemberTestimonial, MemberTestimonialViewModel>>
    {
        public MemberTestimonialDataController(MemberTestimonialRepository repo, IAppSecurityService security)
            :base(repo, security)
            {
                
            }
    }
}
