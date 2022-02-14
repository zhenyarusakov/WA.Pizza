namespace WA.Pizza.Infrastructure.DTO.AdvertisementDTO;

public class CreateAdvertisementRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Img { get; init; }
    public string? WebSite { get; init; }
    public int AdsClientId { get; init; }
}