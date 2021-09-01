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
            var data = (from c in this._ctx.Contracts
                        join am in this._ctx.AccountMemberships
                        on c.AccountMembershipID equals am.Id
                        join a in this._ctx.CRMAccounts
                        on am.AccountID equals a.Id
                        join m in this._ctx.Members
                        on am.MemberID equals m.Id
                        select new { contract = c, accountName = a.AccountName, memberName = m.MemberName })
                        .ToList();
            return data
                .Where(w => selector.Invoke(w.contract))
                .Select(s => new ContractViewModel(s.contract, s.memberName, s.accountName))
                .ToList();
        }

        public override (bool, ContractViewModel) RetrieveById(int id)
        {
            var contract = (from c in this._ctx.Contracts
                        join am in this._ctx.AccountMemberships
                        on c.AccountMembershipID equals am.Id
                        join a in this._ctx.CRMAccounts
                        on am.AccountID equals a.Id
                        join m in this._ctx.Members
                        on am.MemberID equals m.Id
                        where c.Id == id
                        select new ContractViewModel(c, m.MemberName, a.AccountName))
                    .FirstOrDefault();

            return ((contract != null), contract);
        }
    }
}