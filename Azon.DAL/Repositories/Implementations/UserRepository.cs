using Azon.DAL.DAL;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azon.DAL.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByTelegramUserIdAsync(long telegramUserId)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}