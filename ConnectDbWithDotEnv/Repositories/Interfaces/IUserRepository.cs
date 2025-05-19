using ConnectDbWithDotEnv.EntityFramework.Models;

namespace ConnectDbWithDotEnv.Repositories.Interfaces;

public interface IUserRepository : IRepository<UserLogin>
{
  Task<UserLogin?> GetUserByLoginAsync(string login);
}