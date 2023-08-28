using ApotekHjartat.Order.Api;
using ApotekHjartat.Order.Api.Configurations;
using ApotekHjartat.Order.Api.DataTransferObjects.Configuration;
using ApotekHjartat.Order.Api.Repositories.ApotekHjartat;
using ApotekHjartat.Order.Api.Services;
using ApotekHjartat.Order.Api.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AppSettings>(builder.Configuration);

var provider = builder.Services.BuildServiceProvider();
var appSettings = provider.GetRequiredService<IOptions<AppSettings>>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapConfiguration());
});
var mapper = mappingConfig.CreateMapper();

var dbUsername = string.IsNullOrEmpty(builder.Configuration["DbUsername"]) ? appSettings.Value.DbUsername : builder.Configuration["DbUsername"];
var dbPassword = string.IsNullOrEmpty(builder.Configuration["DbPassword"]) ? appSettings.Value.DbPassword : builder.Configuration["DbPassword"];
var dbName = string.IsNullOrEmpty(builder.Configuration["DbName"]) ? appSettings.Value.DbName : builder.Configuration["DbName"];
var dbHostname = appSettings.Value.DbHostname;
var dbPort = appSettings.Value.DbPort;

var con = $"Host={dbHostname};Port={dbPort};Database={dbName};Username={dbUsername};Password={dbPassword}";

Console.WriteLine($"\n\nConnectionString: {con}\n\n");

builder.Services.AddDbContext<ApotekHjartatContext>(options => options.UseNpgsql(con));

builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();

builder.Services.AddScoped<IOrderService, OrderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApotekHjartatContext>();

context.Database.Migrate();
SeedDB.SeedData(context);

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandler();

app.Run();
