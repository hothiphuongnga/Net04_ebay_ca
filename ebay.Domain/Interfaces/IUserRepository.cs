using ebay.Domain.Entities;

namespace ebay.Domain.Repositories;
public interface IUserRepository
{
    Task<User> RegisterUserAsync(User user); 
}