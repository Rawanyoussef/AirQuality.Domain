using AirQuality.Application.Interfaces;
using AirQuality.Application.Services;
using AirQuality.Infrastructure;
using AirQuality.Infrastructure.HostedService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with correct type
builder.Services.AddDbContext<AirContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository
builder.Services.AddScoped<IAirQualitySnapshotRepository, AirQualitySnapshotRepository>();

// Register Factory
builder.Services.AddScoped<IAirQualitySnapshotFactory, AirQualitySnapshotFactory>();

// Register IQAir Client
builder.Services.AddScoped<IQAir>(sp =>
{
    var apiKey = builder.Configuration["IQAir:ApiKey"];
    return new IqAirClient(apiKey);
});

// Register Service
builder.Services.AddScoped<IAirQualityService, AirQualityService>();

// Register HostedService / Cron Job
builder.Services.AddHostedService<ParisAirQualityJob>();

// Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
