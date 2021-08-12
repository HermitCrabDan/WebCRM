namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    /// <summary>
    /// CRM Repository for account data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class CRMAccountRepository: CRMRepositoryBase<CRMAccount, CRMAccountViewModel, CRMDBContext>
    {
        public CRMAccountRepository(CRMDBContext ctx)
            :base(ctx)
            {
            }
            
        public CRMAccountRepository(ILogger logger, CRMDBContext ctx)
            :base(logger, ctx)
            {
            }

    }
}