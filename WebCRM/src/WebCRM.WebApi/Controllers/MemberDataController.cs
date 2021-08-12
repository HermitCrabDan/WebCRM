namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// CRM Api controller for member data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberDataController
        :CRMApiControllerBase<Member,MemberViewModel, ICRMRepository<Member, MemberViewModel>>
    {
        public MemberDataController(ICRMRepository<Member, MemberViewModel> repo, IAppSecurityService security)
            :base(repo, security)
        {

        }

        protected override bool CanDelete()
        {
            return false;
        }

        protected override Func<Member, bool> RestrictedSelection()
        {
            if (this._security.IsMember)
            {
                return n => n.Id == this._security.MemberId;
            }
            return base.RestrictedSelection();
        }

        [HttpPost]
        public override IActionResult Create([FromBody] MemberViewModel model)
        {
            model.MemberAddedBy = this._security.UserID;
            model.MemberAddedDate = DateTime.Now;
            return base.Create(model);
        }

        [HttpPut]
        public override IActionResult Update([FromBody] MemberViewModel model)
        {
            if (model.MemberRemovalDate.HasValue && String.IsNullOrWhiteSpace(model.MemberRemovedBy))
            {
                model.MemberRemovedBy = this._security.UserID;
            }
            return base.Update(model);
        }
    }
}
