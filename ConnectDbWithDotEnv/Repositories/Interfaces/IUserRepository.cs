using ConnectDbWithDotEnv.EntityFramework.Models;

namespace ConnectDbWithDotEnv.Repositories.Interfaces;

public interface IUserRepository : IRepository<Test>
{
  Task<Test?> GetUserByLoginAsync(string login);
}