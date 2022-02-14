namespace WA.Pizza.Infrastructure.DTO.AdvertisementDTO;

public class UpdateAdvertisementRequest
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; init; }
    public string? Img { get; set; }
    public string? WebSite { get; set; }
    public int AdsClientId { get; set; }
}