namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        public override IActionResult Get([FromRoute] int id)
        {
            if (id > 0)
            {
                return Ok(this._repo.Retrieve(n => n.AccountID == id));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}