using ConnectDbWithDotEnv.EntityFramework;
using ConnectDbWithDotEnv.Model;
using ConnectDbWithDotEnv.Repositories.Interfaces;
using ConnectDbWithDotEnv.Repositories.Repositories;
using ConnectDbWithDotEnv.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConnectDbWithDotEnv;

public static class ServiceConfigurator
{
  public static void ConfigureServices(ServiceCollection serviceCollection, ILogger<App> logger)
  {
    logger.LogInformation("Adding environment variables service...");
    serviceCollection.AddSingleton<EnvironmentVariableService>(provider =>
    {
      var serviceLogger = provider.GetRequiredService<ILogger<EnvironmentVariableService>>();
      return new EnvironmentVariableService(serviceLogger);
    });

    serviceCollection.AddDbContext<Context>();
    
    serviceCollection.AddScoped<IRepository<Test>, Repository<Test>>();
  }
}