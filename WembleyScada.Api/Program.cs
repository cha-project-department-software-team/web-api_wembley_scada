using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WembleyScada.Domain.SeedWork;
using WembleyScada.Infrastructure;
using WembleyScada.Api.Application.Mapping;
using WembleyScada.Infrastructure.Communication;
using Buffer = WembleyScada.Api.Application.Workers.Buffer;
using WembleyScada.Api.Application.Workers;
using WembleyScada.Api.Application.Hubs;
using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Infrastructure.Repositories;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("localhost",
                         "http://localhost:3000",
                         "http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration.GetValue("Authority", "");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddSignalR();

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WembleyScada.Api"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));
builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining<ModelToViewModelProfile>();
        cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
        cfg.RegisterServicesFromAssemblyContaining<Entity>();
    });

var config = builder.Configuration;
builder.Services.Configure<MqttOptions>(config.GetSection("MqttOptions"));
builder.Services.AddSingleton<ManagedMqttClient>();
builder.Services.AddSingleton<Buffer>();

builder.Services.AddHostedService<ScadaHost>();

builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IShiftReportRepository, ShiftReportRepository>();
builder.Services.AddScoped<IMachineStatusRepository, MachineStatusRepository>();
builder.Services.AddScoped<IReferenceRepository, ReferenceRepository>();


var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
