using Microsoft.EntityFrameworkCore.Storage;
using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Domain.AggregateModels.PersonAggregate;
using WembleyScada.Domain.AggregateModels.ProductAggregate;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Domain.SeedWork;
using WembleyScada.Infrastructure.EntityConfigurations;

namespace WembleyScada.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "application";

    private IDbContextTransaction? _currentTransaction;
    private readonly IMediator _mediator;

    public DbSet<Device> Devices { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Reference> References { get; set; }
    public DbSet<DeviceReference> DeviceReferences { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<ShiftReport> ShiftReports { get; set; }
    public DbSet<MachineStatus> MachineStatus { get; set; }
    public DbSet<ErrorInformation> ErrorInformations { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationDbContext(DbContextOptions options, IMediator mediator) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeviceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReferenceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LotEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceReferenceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonWorkRecordEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ShiftReportEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MachineStatusEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ErrorInformationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ErrorStatusEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync();

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}