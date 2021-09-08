namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;

    /// <summary>
    /// CRM Api controller for Contract Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    [ApiController]
    [Route("api/[controller]")]
    public class ContractDataController
        :CRMApiControllerBase<Contract, ContractViewModel, ICRMRepository<Contract, ContractViewModel>>
    {
        private CRMDBContext _ctx;

        public ContractDataController(ICRMRepository<Contract, ContractViewModel> repo, IAppSecurityService security, CRMDBContext ctx)
            :base(repo, security)
            {
                this._ctx = ctx;
            }

        protected override Func<Contract, bool> RestrictedSelection()
        {
            if (this._security.IsMember)
            {
                //return n => n.MemberID == this._security.MemberId;
            }
            return base.RestrictedSelection();
        }

        [HttpGet("{id}")]
        public override IActionResult Get([FromRoute] int id)
        {
            var memberId = (this._security.IsMember) ? this._security.MemberId : id;

            var data = (from c in this._ctx.Contracts
                        join am in this._ctx.AccountMemberships
                        on c.AccountMembershipID equals am.Id
                        join a in this._ctx.CRMAccounts
                        on am.AccountID equals a.Id
                        join m in this._ctx.Members
                        on am.MemberID equals m.Id
                        where m.Id == memberId
                        select new { contractData=c, memberName= m.MemberName, accountName= a.AccountName})
                    .ToList();
            if (!this.CanViewAll() && !this._security.IsMember)
            {
                data = data.Where(w => this.RestrictedSelection().Invoke(w.contractData)).ToList();
            }
            return Ok(data.Select(s => new ContractViewModel(s.contractData, s.memberName, s.accountName)));
        }
    }
}