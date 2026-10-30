namespace Azon.Shared.DTOs.MosqueDTOs;

public class MosqueForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? OpenStreetMapId { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
    public long? CityId { get; set; }
    public string? CityName { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}