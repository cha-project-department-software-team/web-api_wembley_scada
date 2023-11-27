using WembleyScada.Domain.SeedWork;
using WembleyScada.Infrastructure.Communication;
using WembleyScada.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Host.Application.Workers;
using WembleyScada.Host.Application.Services;
using WembleyScada.Host.Application.Stores;
using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Infrastructure.Repositories;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<UpdateShiftReportWorker>();
            cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
            cfg.RegisterServicesFromAssemblyContaining<Entity>();
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WembleyScada.Api"));
            options.EnableSensitiveDataLogging();
        });

        var config = builder.Configuration;
        services.Configure<MqttOptions>(config.GetSection("MqttOptions"));
        services.AddSingleton<ManagedMqttClient>();

        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IShiftReportRepository, ShiftReportRepository>();
        services.AddScoped<IMachineStatusRepository, MachineStatusRepository>();

        services.AddSingleton<MetricMessagePublisher>();
        services.AddSingleton<ExecutionTimeBuffers>();
        services.AddHostedService<UpdateShiftReportWorker>();
    })
    .Build();

await host.RunAsync();
