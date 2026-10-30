namespace Azon.Shared.DTOs.CityDTOs;

public class CityForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
    public string TimeZoneId { get; set; } = null!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}