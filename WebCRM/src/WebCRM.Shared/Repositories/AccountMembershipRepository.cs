namespace WebCRM.Shared
{
    using WebCRM.Data;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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

        public override IEnumerable<AccountMembershipViewModel> Retrieve(Func<AccountMembership, bool> selector)
        {
            var data = (from am in this._ctx.AccountMemberships
                        join a in this._ctx.CRMAccounts
                        on am.AccountID equals a.Id
                        join m in this._ctx.Members
                        on am.MemberID equals m.Id
                        select new { membership = am, accountName = a.AccountName, memberName = m.MemberName })
                        .ToList();
            return data
                .Where(w => selector.Invoke(w.membership))
                .Select(s => new AccountMembershipViewModel(s.membership, s.memberName, s.accountName))
                .ToList();
        }

        public override (bool, AccountMembershipViewModel) RetrieveById(int id)
        {
            var contract = (from am in this._ctx.AccountMemberships
                            join a in this._ctx.CRMAccounts
                            on am.AccountID equals a.Id
                            join m in this._ctx.Members
                            on am.MemberID equals m.Id
                            where am.Id == id
                            select new AccountMembershipViewModel(am, m.MemberName, a.AccountName))
                    .FirstOrDefault();

            return ((contract != null), contract);
        }
    }
}