using ConnectDbWithDotEnv.EntityFramework;
using ConnectDbWithDotEnv.EntityFramework.Models;
using ConnectDbWithDotEnv.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConnectDbWithDotEnv.Repositories.Repositories;

public class UserRepository(Context context) : Repository<UserLogin>(context), IUserRepository
{
  public async Task<UserLogin?> GetUserByLoginAsync(string login)
  {
    return await _dbSet.FirstOrDefaultAsync(user => user.FullLogin == login);
  }
}