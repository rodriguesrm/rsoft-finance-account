﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RSoft.Entry.Infra;

namespace RSoft.Entry.Infra.Migrations
{

    [ExcludeFromCodeCoverage(Justification = "Migrations are automatically generated by .net")]
    [DbContext(typeof(AccountContext))]
    partial class AccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ChangedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ChangedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ChangedBy")
                        .HasDatabaseName("IX_Account_ChangedBy");

                    b.HasIndex("ChangedOn")
                        .HasDatabaseName("IX_Account_ChangedOn");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("IX_Account_CreatedBy");

                    b.HasIndex("CreatedOn")
                        .HasDatabaseName("IX_Account_CreatedOn");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("AK_Account_Name");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.AccrualPeriod", b =>
                {
                    b.Property<short>("Year")
                        .HasColumnType("smallint")
                        .HasColumnName("Year");

                    b.Property<sbyte>("Month")
                        .HasColumnType("tinyint")
                        .HasColumnName("Month");

                    b.Property<Guid?>("ChangedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ChangedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("IsClosed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul)
                        .HasColumnName("IsClosed");

                    b.Property<float>("OpeningBalance")
                        .HasColumnType("float")
                        .HasColumnName("OpeningBalance");

                    b.Property<float>("TotalCredits")
                        .HasColumnType("float")
                        .HasColumnName("TotalCredits");

                    b.Property<float>("TotalDebts")
                        .HasColumnType("float")
                        .HasColumnName("TotalDebts");

                    b.Property<Guid?>("UserIdClosed")
                        .HasColumnType("char(36)");

                    b.HasKey("Year", "Month");

                    b.HasIndex("ChangedBy")
                        .HasDatabaseName("IX_AccrualPeriod_ChangedBy");

                    b.HasIndex("ChangedOn")
                        .HasDatabaseName("IX_AccrualPeriod_ChangedOn");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("IX_AccrualPeriod_CreatedBy");

                    b.HasIndex("CreatedOn")
                        .HasDatabaseName("IX_AccrualPeriod_CreatedOn");

                    b.HasIndex("UserIdClosed")
                        .HasDatabaseName("IX_AccrualPeriod_UserIdClosed");

                    b.ToTable("AccrualPeriod");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ChangedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ChangedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("ChangedBy")
                        .HasDatabaseName("IX_Category_ChangedBy");

                    b.HasIndex("ChangedOn")
                        .HasDatabaseName("IX_Category_ChangedOn");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("IX_Category_CreatedBy");

                    b.HasIndex("CreatedOn")
                        .HasDatabaseName("IX_Category_CreatedOn");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("AK_Category_Name");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ChangedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ChangedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)")
                        .HasColumnName("Name");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int")
                        .HasColumnName("PaymentType");

                    b.HasKey("Id");

                    b.HasIndex("ChangedBy")
                        .HasDatabaseName("IX_PaymentMethod_ChangedBy");

                    b.HasIndex("ChangedOn")
                        .HasDatabaseName("IX_PaymentMethod_ChangedOn");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("IX_PaymentMethod_CreatedBy");

                    b.HasIndex("CreatedOn")
                        .HasDatabaseName("IX_PaymentMethod_CreatedOn");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("AK_PaymentMethod_Name");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(16,2)")
                        .HasColumnName("Amount");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("Date");

                    b.Property<sbyte>("Month")
                        .HasColumnType("tinyint")
                        .HasColumnName("Month");

                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnType("char(36)");

                    b.Property<sbyte>("TransactionType")
                        .HasColumnType("tinyint")
                        .HasColumnName("TransactionType");

                    b.Property<short>("Year")
                        .HasColumnType("smallint")
                        .HasColumnName("Year");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("IX_Transaction_CreatedBy");

                    b.HasIndex("CreatedOn")
                        .HasDatabaseName("IX_Transaction_CreatedOn");

                    b.HasIndex("Date")
                        .HasDatabaseName("IX_Transaction_Date");

                    b.HasIndex("Month")
                        .HasDatabaseName("IX_Transaction_Month");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("Year")
                        .HasDatabaseName("IX_Transaction_Year");

                    b.HasIndex("Year", "Month");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<ulong>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("LastName");

                    b.HasKey("Id");

                    b.HasIndex("FirstName", "LastName")
                        .HasDatabaseName("IX_User_FullName");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Account", b =>
                {
                    b.HasOne("RSoft.Entry.Infra.Tables.Category", "Category")
                        .WithMany("Accounts")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Account_CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RSoft.Entry.Infra.Tables.User", "ChangedAuthor")
                        .WithMany("ChangedAccounts")
                        .HasForeignKey("ChangedBy")
                        .HasConstraintName("FK__User_Account_ChangedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RSoft.Entry.Infra.Tables.User", "CreatedAuthor")
                        .WithMany("CreatedAccounts")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_User_Account_CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ChangedAuthor");

                    b.Navigation("CreatedAuthor");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.AccrualPeriod", b =>
                {
                    b.HasOne("RSoft.Entry.Infra.Tables.User", "ChangedAuthor")
                        .WithMany("ChangedAccrualPeriods")
                        .HasForeignKey("ChangedBy")
                        .HasConstraintName("FK_User_AccrualPeriod_ChangedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RSoft.Entry.Infra.Tables.User", "CreatedAuthor")
                        .WithMany("CreatedAccrualPeriods")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_User_AccrualPeriod_CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RSoft.Entry.Infra.Tables.User", "ClosedAuthor")
                        .WithMany("ClosedAccrualPeriods")
                        .HasForeignKey("UserIdClosed")
                        .HasConstraintName("FK_User_AccrualPeriod_UserIdClosed")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ChangedAuthor");

                    b.Navigation("ClosedAuthor");

                    b.Navigation("CreatedAuthor");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Category", b =>
                {
                    b.HasOne("RSoft.Entry.Infra.Tables.User", "ChangedAuthor")
                        .WithMany("ChangedCategories")
                        .HasForeignKey("ChangedBy")
                        .HasConstraintName("FK_User_Category_ChangedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RSoft.Entry.Infra.Tables.User", "CreatedAuthor")
                        .WithMany("CreatedCategories")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_User_Category_CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChangedAuthor");

                    b.Navigation("CreatedAuthor");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.PaymentMethod", b =>
                {
                    b.HasOne("RSoft.Entry.Infra.Tables.User", "ChangedAuthor")
                        .WithMany("ChangedPaymentMethods")
                        .HasForeignKey("ChangedBy")
                        .HasConstraintName("FK_User_PaymentMethod_ChangedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RSoft.Entry.Infra.Tables.User", "CreatedAuthor")
                        .WithMany("CreatedPaymentMethods")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_User_PaymentMethod_CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChangedAuthor");

                    b.Navigation("CreatedAuthor");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Transaction", b =>
                {
                    b.HasOne("RSoft.Entry.Infra.Tables.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_User_Transaction_AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RSoft.Entry.Infra.Tables.User", "CreatedAuthor")
                        .WithMany("CreatedTransactions")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_User_Transaction_CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RSoft.Entry.Infra.Tables.PaymentMethod", "PaymentMethod")
                        .WithMany("Transactions")
                        .HasForeignKey("PaymentMethodId")
                        .HasConstraintName("FK_User_Transaction_PaymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RSoft.Entry.Infra.Tables.AccrualPeriod", "AccrualPeriod")
                        .WithMany("Transactions")
                        .HasForeignKey("Year", "Month")
                        .HasConstraintName("FK_AccrualPeriod_Transaction_YearMonth")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("AccrualPeriod");

                    b.Navigation("CreatedAuthor");

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.AccrualPeriod", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.Category", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.PaymentMethod", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("RSoft.Entry.Infra.Tables.User", b =>
                {
                    b.Navigation("ChangedAccounts");

                    b.Navigation("ChangedAccrualPeriods");

                    b.Navigation("ChangedCategories");

                    b.Navigation("ChangedPaymentMethods");

                    b.Navigation("ClosedAccrualPeriods");

                    b.Navigation("CreatedAccounts");

                    b.Navigation("CreatedAccrualPeriods");

                    b.Navigation("CreatedCategories");

                    b.Navigation("CreatedPaymentMethods");

                    b.Navigation("CreatedTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
