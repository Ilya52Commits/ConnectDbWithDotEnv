using System.IO;
using DotNetEnv;
using Microsoft.Extensions.Logging;

namespace ConnectDbWithDotEnv.Services;

public class EnvironmentVariableService
{
  public EnvironmentVariableService(ILogger<EnvironmentVariableService> logger)
  {
    var pathDotEnv = Path.Combine(Directory.GetCurrentDirectory(), ".env");

    if (File.Exists(pathDotEnv))
    {
      logger.LogInformation("Checking the existence of a file...");
      Env.Load(pathDotEnv);
    }
    else
    {
      logger.LogInformation("An attempt to download a file from the project root...");
      var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())?
        .Parent?
        .Parent?
        .FullName;

      if (!string.IsNullOrEmpty(projectRoot))
      {
        pathDotEnv = Path.Combine(projectRoot, ".env");

        if (File.Exists(pathDotEnv))
        {
          logger.LogInformation("Uploading the .env file...");
          Env.Load(pathDotEnv);
        }
        
      }
      else
      {
        logger.LogError("I couldn't find the file!"); 
        throw new FileNotFoundException("Не найден .env файл");
      }
    }
  }

  public string GetEnvironmentVariable(string variableName)
  {
    return Env.GetString(variableName);
  }
}