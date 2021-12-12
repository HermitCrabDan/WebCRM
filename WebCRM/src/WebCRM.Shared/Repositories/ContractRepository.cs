namespace WebCRM.Shared
{
    using WebCRM.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

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

        public override async Task<IEnumerable<ContractViewModel>> RetrieveAsync(Expression<Func<Contract, bool>> selector)
        {
            var data = await (from c in this._ctx.Contracts
                              .Where(selector)
                        join am in this._ctx.AccountMemberships
                        on c.AccountMembershipID equals am.Id
                        join a in this._ctx.CRMAccounts
                        on am.AccountID equals a.Id
                        join m in this._ctx.Members
                        on am.MemberID equals m.Id
                        select new { contract = c, accountName = a.AccountName, memberName = m.MemberName })
                        .ToListAsync();

            return data
                .Select(s => new ContractViewModel(s.contract, s.memberName, s.accountName))
                .ToList();
        }

        public override async Task<(bool, ContractViewModel)> RetrieveByIdAsync(int id)
        {
            var contract = await (from c in this._ctx.Contracts
                        join am in this._ctx.AccountMemberships
                        on c.AccountMembershipID equals am.Id
                        join a in this._ctx.CRMAccounts
                        on am.AccountID equals a.Id
                        join m in this._ctx.Members
                        on am.MemberID equals m.Id
                        where c.Id == id
                        select new ContractViewModel(c, m.MemberName, a.AccountName))
                    .FirstOrDefaultAsync();

            return ((contract != null), contract);
        }
    }
}