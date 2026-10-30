namespace Azon.Shared.DTOs.MosqueDTOs;

public class MosqueForShortResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public long? CityId { get; set; }
}