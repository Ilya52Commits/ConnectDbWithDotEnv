using System.IO;
using System.Windows;
using ConnectDbWithDotEnv.MVVM.ViewModels;
using ConnectDbWithDotEnv.MVVM.Views;
using ConnectDbWithDotEnv.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConnectDbWithDotEnv;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
  protected override async void OnStartup(StartupEventArgs e)
  {
    try
    {
      var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
    
      var logDirectory = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Logs",
        DateTime.Now.Year.ToString(),
        DateTime.Now.Month.ToString("00"),
        DateTime.Now.Day.ToString("00"));
    
      Directory.CreateDirectory(logDirectory);
    
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .WriteTo.File(
          Path.Combine(logDirectory, $"AuthLogs_{DateTime.Now:HHmmss}.log"),
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
          rollingInterval: RollingInterval.Infinite) // Отключаем авторотацию
        .CreateLogger();

      var serviceCollection = new ServiceCollection();

      serviceCollection.AddLogging(logging =>
      {
        logging.AddSerilog();
      });

      var loggerFactory = LoggerFactory.Create(builder =>
      {
        builder.AddSerilog();
      });
    
      var logger = loggerFactory.CreateLogger<App>();
    
      logger.LogInformation("Logging setup completed");
      
      // Регистрируем все сервисы заранее
      ServiceConfigurator.ConfigureServices(serviceCollection, logger);
      serviceCollection.AddTransient<ErrorViewModel>();
      serviceCollection.AddTransient<ErrorView>();
      serviceCollection.AddSingleton<TestView>();
      
      var authService = serviceCollection.BuildServiceProvider().GetRequiredService<AuthorizationService>(); 
      var isAuthorized = await authService.IsAuthorized();

      if (!isAuthorized)
      {
        var errorView = serviceCollection.BuildServiceProvider().GetRequiredService<ErrorView>();
        errorView.Show();
      }
      else
      {
        var mainWindow = new MainView(serviceCollection.BuildServiceProvider().GetRequiredService<TestView>());
        MainWindow = mainWindow;
        MainWindow.Show();
      }
    }
    catch (Exception ex)
    {
      throw ;
    }
  }
}