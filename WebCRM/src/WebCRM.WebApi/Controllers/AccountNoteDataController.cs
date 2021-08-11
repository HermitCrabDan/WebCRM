namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Api controller for account note data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountNoteDataController
        :CRMApiControllerBase<AccountNote, AccountNoteViewModel, ICRMRepository<AccountNote, AccountNoteViewModel>> 
    {
        public AccountNoteDataController(ICRMRepository<AccountNote, AccountNoteViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {

            }
    }
}