using ConnectDbWithDotEnv.Model;
using ConnectDbWithDotEnv.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConnectDbWithDotEnv.EntityFramework;

public sealed class Context : DbContext
{
  // поменять
  public DbSet<Test> Tests { get; set; }
  
  private readonly EnvironmentVariableService _environmentVariableService;
  private readonly ILogger<Context> _logger;
  
  public Context(EnvironmentVariableService environmentVariableService, ILogger<Context> logger)
  {
    _environmentVariableService = environmentVariableService;
    _logger = logger;
    Database.EnsureCreated();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    _logger.LogInformation("Getting the connection string from the environment variables...");
    var connectionString = _environmentVariableService.GetEnvironmentVariable("DB_CONNECTION");

    _logger.LogInformation("Using the connection string to configure the Context...");
    optionsBuilder.UseSqlServer(connectionString);
    _logger.LogInformation("Connection string applied");
        
    _logger.LogInformation("Попытка подключения к SQL Server...");

    using var connection = new SqlConnection(connectionString);
    try
    {
      connection.Open();
                
      _logger.LogInformation("Успешное подключение к SQL Server");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Ошибка подключения к бд");
      throw; 
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    var test1 = new Test {Id = 1, Name = "Вывожу какой-то текст"};
    modelBuilder.Entity<Test>().HasData(test1);
    base.OnModelCreating(modelBuilder);
  }
}