namespace Azon.Shared.DTOs.CityDTOs;

public class CityForShortResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}