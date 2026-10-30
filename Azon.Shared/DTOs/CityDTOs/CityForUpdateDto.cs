namespace Azon.Shared.DTOs.CityDTOs;

public class CityForUpdateDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string CountryCode { get; set; }
    public required string TimeZoneId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsActive { get; set; }
}