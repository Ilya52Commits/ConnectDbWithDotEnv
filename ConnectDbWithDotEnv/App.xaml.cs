using System.IO;
using System.Windows;
using ConnectDbWithDotEnv.MVVM.ViewModels;
using ConnectDbWithDotEnv.MVVM.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConnectDbWithDotEnv;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
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
    
    ServiceConfigurator.ConfigureServices(serviceCollection, logger);
    
    logger.LogInformation("Adding TestViewModel...");
    serviceCollection.AddTransient<TestViewModel>();
    logger.LogInformation("Adding TestView...");
    serviceCollection.AddSingleton<TestView>();
    
    var mainWindow = new MainView(serviceCollection.BuildServiceProvider().GetRequiredService<TestView>());
    
    MainWindow = mainWindow;
    
    MainWindow.Show();
  }
}