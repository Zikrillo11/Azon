using Azon.DAL.DAL;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Azon.DAL.Repositories.Implementations;

public class NotificationLogRepository : INotificationLogRepository
{
    private readonly AppDbContext _context;

    public NotificationLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationLog?> GetByIdAsync(long id)
    {
        return await _context.NotificationLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<NotificationLog>> GetByUserIdAsync(long userId)
    {
        return await _context.NotificationLogs
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.PrayerDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<NotificationLog>> GetPendingNotificationsAsync(DateOnly date)
    {
        return await _context.NotificationLogs
            .AsNoTracking()
            .Where(x =>
                x.PrayerDate == date &&
                x.Status == NotificationStatus.Pending)
            .ToListAsync();
    }

    public async Task<NotificationLog> CreateAsync(
        NotificationLog notificationLog)
    {
        await _context.NotificationLogs.AddAsync(notificationLog);
        await _context.SaveChangesAsync();

        return notificationLog;
    }

    public async Task<NotificationLog> UpdateAsync(
        NotificationLog notificationLog)
    {
        _context.NotificationLogs.Update(notificationLog);
        await _context.SaveChangesAsync();

        return notificationLog;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var notificationLog = await _context.NotificationLogs.FindAsync(id);

        if (notificationLog is null)
            return false;

        _context.NotificationLogs.Remove(notificationLog);
        await _context.SaveChangesAsync();

        return true;
    }
}