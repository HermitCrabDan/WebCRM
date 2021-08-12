namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.RoleSecurity;
    using WebCRM.Shared;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// CRM Api controller for Account Membership Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountMembershipDataController
        :CRMApiControllerBase<AccountMembership, AccountMembershipViewModel, ICRMRepository<AccountMembership,AccountMembershipViewModel>>
    {
        public AccountMembershipDataController(ICRMRepository<AccountMembership, AccountMembershipViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {
                
            }

        protected override bool CanDelete()
        {
            return false;
        }

        protected override Func<AccountMembership, bool> RestrictedSelection()
        {
            if (this._security.IsMember)
            {
                return n => n.MemberID == this._security.MemberId;
            }
            return base.RestrictedSelection();
        }

        [HttpPost]
        public override IActionResult Create([FromBody] AccountMembershipViewModel model)
        {
            model.MembershipCreatedBy = this._security.UserID;
            model.MembershipCreationDate = DateTime.Now;
            return base.Create(model);
        }

        [HttpPut]
        public override IActionResult Update([FromBody] AccountMembershipViewModel model)
        {
            model.LastUpdatedBy = this._security.UserID;
            model.LastUpdatedDate = DateTime.Now;
            if (model.MembershipRemovalDate.HasValue && string.IsNullOrWhiteSpace(model.MembershipRemovedBy))
            {
                model.MembershipRemovedBy = this._security.UserID;
            }
            return base.Update(model);
        }
    }
}