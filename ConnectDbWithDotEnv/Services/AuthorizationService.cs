using System.Security.Principal;
using ConnectDbWithDotEnv.Repositories.Interfaces;

namespace ConnectDbWithDotEnv.Services;

public class AuthorizationService(IUserRepository repository)
{
  // Событие уведомления о присваивании сообщения об ошибке
  public event AssigningAnErrorMessage? ErrorMessage;

  // Делегат для события
  public delegate void AssigningAnErrorMessage(string errorMessage);
  
  public async Task<bool> IsAuthorized()
  {
    var identity = WindowsIdentity.GetCurrent();
    var domainLogin = identity.Name; // "DOMAIN\login"
    
    var user = await repository.GetUserByLoginAsync(domainLogin);

    if (user == null)
    {
      ErrorMessage?.Invoke("Пользователь не найден в системе. Обратитесь к администратору.");
      return false;
    }
    
    user.LastLogin = DateTime.Now;
    
    repository.UpdateAsync(user);

    return true;
  } 
}