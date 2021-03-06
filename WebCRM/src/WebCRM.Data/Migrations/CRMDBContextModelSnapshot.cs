// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCRM.Data;

namespace WebCRM.Data.Migrations
{
    [DbContext(typeof(CRMDBContext))]
    partial class CRMDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("WebCRM.Data.AccountMembership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPrimaryAccountMember")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("MemberID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountID", "MemberID");

                    b.ToTable("AccountMembership");
                });

            modelBuilder.Entity("WebCRM.Data.AccountNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("NoteText")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountID");

                    b.ToTable("AccountNote");
                });

            modelBuilder.Entity("WebCRM.Data.CRMAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CRMAccount");
                });

            modelBuilder.Entity("WebCRM.Data.CRMUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CRMUser");
                });

            modelBuilder.Entity("WebCRM.Data.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountMembershipID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ContractAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ContractEndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContractName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastExpensePaymentDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastPaymentRecievedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OriginalContractID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PaymentDate")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalExpenseAmount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalPaidAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountMembershipID");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("WebCRM.Data.ContractExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContractID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ExpenseAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpenseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContractID");

                    b.ToTable("ContractExpense");
                });

            modelBuilder.Entity("WebCRM.Data.ContractTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContractID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("FeeAmount")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentMonth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PaymentYear")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContractID");

                    b.ToTable("ContractTransaction");
                });

            modelBuilder.Entity("WebCRM.Data.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeletionBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Member");
                });
#pragma warning restore 612, 618
        }
    }
}
