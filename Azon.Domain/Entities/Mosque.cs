
using Azon.Domain.Common;

namespace Azon.Domain.Entities;

public class Mosque : BaseEntity
{
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? OpenStreetMapId { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; } = true;
    public long? CityId { get; set; }
    public City? City { get; set; }
}
