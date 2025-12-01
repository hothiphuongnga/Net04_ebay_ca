using ebay.Infrastructure.Data;
using ebay.Domain.Entities;
using ebay.Domain.Repositories;

namespace ebay.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EBayDbContext _context;

    public UserRepository(EBayDbContext context)
    {
        _context = context;
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await  _context.SaveChangesAsync();
        return user;
    }
}