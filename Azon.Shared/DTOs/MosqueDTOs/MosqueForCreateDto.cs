namespace Azon.Shared.DTOs.MosqueDTOs;

public class MosqueForCreateDto
{
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? OpenStreetMapId { get; set; }
    public long? CityId { get; set; }
}