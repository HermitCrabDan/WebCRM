namespace WebCRM.WebApi.Controllers
{
    using System;
    using WebCRM.Data;
    using WebCRM.RoleSecurity;
    using WebCRM.Shared.ViewModels;
    using WebCRM.Shared.Repositories;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class RemittanceSummaryDataController: ControllerBase
    {
        private RemittanceRepository _repo;
        private IAppSecurityService _security;

        public RemittanceSummaryDataController(CRMDBContext ctx, IAppSecurityService securityService)
        {
            this._repo = new RemittanceRepository(ctx);
            this._security = securityService;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(this._repo.GetMonthlyRemittanceSummary(id));
        }
    }
}
