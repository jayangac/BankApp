using BankApp.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data.BaseOperation;

public partial class BankDbContext : DbContext
{
    public BankDbContext()
    {
    }

    public BankDbContext(DbContextOptions<BankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=JAYANGA;Database=BankDb;User ID=sa;Password=Abc123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.ToTable("Rule");

            entity.Property(e => e.Rate).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.RuleDate).HasColumnType("date");
            entity.Property(e => e.RuleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactDate).HasColumnType("date");
            entity.Property(e => e.TransactType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TxnId)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Transaction_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
