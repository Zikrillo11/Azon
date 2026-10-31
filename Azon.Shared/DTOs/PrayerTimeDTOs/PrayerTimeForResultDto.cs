namespace Azon.Shared.DTOs.PrayerTimeDTOs;

public class PrayerTimeForResultDto
{
    public long Id { get; set; }
    public DateOnly PrayerDate { get; set; }
    public TimeOnly Fajr { get; set; }
    public TimeOnly Dhuhr { get; set; }
    public TimeOnly Asr { get; set; }
    public TimeOnly Maghrib { get; set; }
    public TimeOnly Isha { get; set; }
}