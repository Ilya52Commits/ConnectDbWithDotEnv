using System.Security.Principal;
using ConnectDbWithDotEnv.Repositories.Interfaces;

namespace ConnectDbWithDotEnv.Services;

public class AuthorizationService(IUserRepository repository)
{
  public async Task<bool> IsAuthorized()
  {
    var identity = WindowsIdentity.GetCurrent();
    var domainLogin = identity.Name;
        
    var user = await repository.GetUserByLoginAsync(domainLogin);

    if (user == null)
    {
      return false;
    }
        
    user.LastAccess = DateTime.Now;
    await repository.UpdateAsync(user);
    return true;
  }
}