namespace WebCRM.Shared
{
    using WebCRM.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// CRM repository for contract data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class ContractRepository: CRMRepositoryBase<Contract, ContractViewModel, CRMDBContext>
    {
        public ContractRepository(CRMDBContext ctx)
            :base(ctx)
            {
            }

        public ContractRepository(ILogger logger, CRMDBContext ctx)
            :base(logger, ctx)
            {
            }

        public override IEnumerable<ContractViewModel> Retrieve(Func<Contract, bool> selector)
        {
            //var data = this._ctx.Contracts.Where(selector).ToList();
            var data = (from c in this._ctx.Contracts
                        join m in this._ctx.Members
                        on c.MemberID equals m.Id
                        join a in this._ctx.CRMAccounts
                        on c.AccountID equals a.Id
                        select new { contract = c, accountName = a.AccountName, memberName = m.MemberName })
                        .ToList();
            return data
                .Where(w => selector.Invoke(w.contract))
                .Select(s => new ContractViewModel(s.contract, s.memberName, s.accountName))
                .ToList();
        }
    }
}