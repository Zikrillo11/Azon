namespace Azon.Shared.DTOs.CityDTOs;

public class CityForCreateDto
{
    public required string Name { get; set; }
    public required string CountryCode { get; set; }
    public required string TimeZoneId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}