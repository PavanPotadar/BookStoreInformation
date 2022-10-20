using BooksInformation.DbContexts;
 
using BooksInformation.Repository;
using Serilog;
using Microsoft.EntityFrameworkCore;
using BooksInformation.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Logs/BooksInfoLogs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
 
builder.Services.AddScoped<IBookInfoRepository, BookInfoRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookInfoContext>(
    dbContextOption => dbContextOption.UseSqlServer(builder
    .Configuration["ConnectionStrings:BookStoreDBCOnnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHealthChecks().AddCheck("DB Health Check", () => DBHealthCheckProvider.Check(builder
    .Configuration["ConnectionStrings:BookStoreDBCOnnectionString"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

 
app.MapHealthChecks("api/health");
app.MapControllers();

app.Run();

public partial class Program { }
