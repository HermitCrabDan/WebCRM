namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    /// <summary>
    /// CRM Repository for account membership data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class AccountMembershipRepository
        :CRMRepositoryBase<AccountMembership, AccountMembershipViewModel, CRMDBContext>
    {
        public AccountMembershipRepository(CRMDBContext context)
            :base(context)
            {
            }

        public AccountMembershipRepository(ILogger logger, CRMDBContext context)
            :base(logger, context)
            {
            }

        public override async Task<IEnumerable<AccountMembershipViewModel>> RetrieveAsync(Expression<Func<AccountMembership, bool>> selector)
        {
            var data = await (from am in this._ctx.AccountMemberships
                              .Where(selector)
                        join a in this._ctx.CRMAccounts
                        on am.AccountID equals a.Id
                        join m in this._ctx.Members
                        on am.MemberID equals m.Id
                        select new { membership = am, accountName = a.AccountName, memberName = m.MemberName })
                        .ToListAsync();
            return data
                .Select(s => new AccountMembershipViewModel(s.membership, s.memberName, s.accountName))
                .ToList();
        }

        public override async Task<(bool, AccountMembershipViewModel)> RetrieveByIdAsync(int id)
        {
            var contract = await (from am in this._ctx.AccountMemberships
                            join a in this._ctx.CRMAccounts
                            on am.AccountID equals a.Id
                            join m in this._ctx.Members
                            on am.MemberID equals m.Id
                            where am.Id == id
                            select new AccountMembershipViewModel(am, m.MemberName, a.AccountName))
                    .FirstOrDefaultAsync();

            return ((contract != null), contract);
        }
    }
}