using ConnectDbWithDotEnv.EntityFramework;
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
    
    logger.LogInformation("Adding repository...");
    
    serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    serviceCollection.AddScoped<IUserRepository, UserRepository>(); 
    
    serviceCollection.AddSingleton<AuthorizationService>();
  }
}