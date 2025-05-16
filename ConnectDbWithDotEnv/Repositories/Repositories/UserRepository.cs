using ConnectDbWithDotEnv.EntityFramework;
using ConnectDbWithDotEnv.EntityFramework.Models;
using ConnectDbWithDotEnv.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConnectDbWithDotEnv.Repositories.Repositories;

public class UserRepository(Context context) : Repository<Test>(context), IUserRepository
{
  public async Task<Test?> GetUserByLoginAsync(string login)
  {
    return await _dbSet.FirstOrDefaultAsync(user => user.Login == login);
  }
}