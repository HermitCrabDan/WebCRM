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
    /// CRM Api controller for Contract Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    [ApiController]
    [Route("api/[controller]")]
    public class ContractDataController
        :CRMApiControllerBase<Contract, ContractViewModel, ICRMRepository<Contract, ContractViewModel>>
    {
        public ContractDataController(ICRMRepository<Contract, ContractViewModel> repo, IAppSecurityService security)
            :base(repo, security)
            {
                
            }

        protected override bool CanDelete()
        {
            return false;
        }

        protected override Func<Contract, bool> RestrictedSelection()
        {
            if (this._security.IsMember)
            {
                return n => n.MemberID == this._security.MemberId;
            }
            return base.RestrictedSelection();
        }

        [HttpPost]
        public override IActionResult Create([FromBody] ContractViewModel model)
        {
            model.CreatedBy = this._security.UserID;
            model.CreationDate = DateTime.Now;
            return base.Create(model);
        }

        [HttpPut]
        public override IActionResult Update([FromBody] ContractViewModel model)
        {
            model.LastUpdatedBy = this._security.UserID;
            model.LastUpdatedDate = DateTime.Now;
            return base.Update(model);
        }

    }
}