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
    public class MemberContractDataController:ControllerBase
    {
        private CRMDBContext _ctx;
        private IAppSecurityService _security

        public MemberContractDataController(IAppSecurityService security, CRMDBContext ctx)
        {
            this._security = security;
            this._ctx = ctx;
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
                        select new ContractViewModel(c, m.MemberName, a.AccountName))
                    .ToList();
                    
            return Ok(data);
        }
    }
}