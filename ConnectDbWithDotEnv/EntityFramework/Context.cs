using ConnectDbWithDotEnv.EntityFramework.Models;
using ConnectDbWithDotEnv.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConnectDbWithDotEnv.EntityFramework;

public sealed class Context : DbContext
{
  // поменять
  public DbSet<UserLogin> UserLogins { get; set; }
  
  private readonly EnvironmentVariableService _environmentVariableService;
  private readonly ILogger<Context> _logger;
  
  
  public Context(DbContextOptions<Context> options, EnvironmentVariableService environmentVariableService, ILogger<Context> logger):base(options)
  {
    _environmentVariableService = environmentVariableService;
    _logger = logger;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    _logger.LogInformation("Getting the connection string from the environment variables...");
    var connectionString = _environmentVariableService.GetEnvironmentVariable("DB_CONNECTION");

    _logger.LogInformation("Using the connection string to configure the Context...");
    optionsBuilder.UseSqlServer(connectionString);
    _logger.LogInformation("Connection string applied");
        
    _logger.LogInformation("Попытка подключения к SQL Server...");

      /*using var connection = new SqlConnection(connectionString);
    try
    {
      connection.Open();
                
      _logger.LogInformation("Успешное подключение к SQL Server");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Ошибка подключения к бд");
      throw; 
    }*/
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
  
    base.OnModelCreating(modelBuilder);
  }
}