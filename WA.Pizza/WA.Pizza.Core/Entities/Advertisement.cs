namespace WA.Pizza.Core.Entities;

public class Advertisement
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Img { get; init; }
    public string? WebSite { get; init; }
    public int AdsClientId { get; set; }
    public AdsClient? AdsClient { get; set; }
}