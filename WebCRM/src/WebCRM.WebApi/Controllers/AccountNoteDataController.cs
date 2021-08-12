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

        protected override bool CanDelete()
        {
            return false;
        }

        [HttpPost]
        public override IActionResult Create([FromBody] AccountNoteViewModel model)
        {
            model.CreatedBy = this._security.UserID;
            model.NoteCreationDate = System.DateTime.Now;
            return base.Create(model);
        }

        [HttpPut]
        public override IActionResult Update([FromBody] AccountNoteViewModel model)
        {
            if (model.NoteRemovalDate.HasValue && string.IsNullOrWhiteSpace(model.NoteRemovedBy))
            {
                model.NoteRemovedBy = this._security.UserID;
            }
            return base.Update(model);
        }
    }
}