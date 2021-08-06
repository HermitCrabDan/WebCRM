namespace WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    /// <summary>
    /// 
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class CRMDBContext: DbContext
    { 
        public CRMDBContext()
            :base()
        {
        }

        public CRMDBContext(DbContextOptions<CRMDBContext> options)
            :base(options)
        {
        }

        public DbSet<CRMAccount> CRMAccounts { get; set; }

        public DbSet<AccountMembership> AccountMemberships { get; set; }

        public DbSet<AccountNote> AccountNotes { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractTransaction> ContractTransactions { get; set; }

        public DbSet<ContractExpense> ContractExpenses { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<MemberTestimonial> MemberTestimonials { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<CRMAccount>().HasKey(k => k.Id);
            modelBuilder.Entity<AccountMembership>().HasKey(k => k.Id);
            modelBuilder.Entity<AccountNote>().HasKey(k => k.Id);
            modelBuilder.Entity<Contract>().HasKey(k => k.Id);
            modelBuilder.Entity<ContractTransaction>().HasKey(k => k.Id);
            modelBuilder.Entity<ContractExpense>().HasKey(k => k.Id);
            modelBuilder.Entity<Member>().HasKey(k => k.Id);
            modelBuilder.Entity<MemberTestimonial>().HasKey(k => k.Id);

            modelBuilder.Entity<AccountMembership>().HasIndex(k => new { k.AccountID, k.MemberID });
            modelBuilder.Entity<AccountNote>().HasIndex(k => k.AccountID);
            modelBuilder.Entity<ContractTransaction>().HasIndex(k => k.ContractID);
            modelBuilder.Entity<ContractExpense>().HasIndex(k => k.ContractID);
            modelBuilder.Entity<MemberTestimonial>().HasIndex(k => k.MemberID);
            modelBuilder.Entity<Contract>().HasKey(k => new { k.AccountID, k.MemberID });

            modelBuilder.Entity<CRMAccount>().ToTable("CRMAccount");
            modelBuilder.Entity<AccountMembership>().ToTable("AccountMembership");
            modelBuilder.Entity<AccountNote>().ToTable("AccountNote");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<ContractTransaction>().ToTable("ContractTransaction");
            modelBuilder.Entity<ContractExpense>().ToTable("ContractExpense");
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<MemberTestimonial>().ToTable("MemberTestimonial");
        }
    }    
}