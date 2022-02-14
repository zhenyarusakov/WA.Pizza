using WA.Pizza.Infrastructure.DTO.AdsClientDTO;

namespace WA.Pizza.Infrastructure.DTO.AdvertisementDTO;

public class AdvertisementDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Img { get; set; }
    public string? WebSite { get; set; }
    public int AdsClientId { get; }
    public AdsClientDto ClientDto{get; set; }
}