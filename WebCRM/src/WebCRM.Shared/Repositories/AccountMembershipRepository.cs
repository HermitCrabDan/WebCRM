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
            var data = this._ctx.AccountMemberships.Where(selector).ToList();

            var members = this._ctx.Members.ToList();
            var membershiplist = (from d in data
                                join m in members
                                on d.MemberID equals m.Id
                                select new AccountMembershipViewModel(d, m.MemberName)
                                ).ToList();
            return membershiplist;
        }

        public override async Task<IEnumerable<AccountMembershipViewModel>> RetrieveAsync(Func<AccountMembership, bool> selector)
        {
            var data = this._ctx.AccountMemberships.Where(selector).AsQueryable();
            var membershipList = await (from d in data
                                  join m in _ctx.Members
                                  on d.MemberID equals m.Id
                                  select new AccountMembershipViewModel(d, m.MemberName)
                                  ).ToListAsync();
            return membershipList;
        }
    }
}