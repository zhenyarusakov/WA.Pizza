using System;

namespace WA.Pizza.Infrastructure.DTO.AdsClientDTO;

public class UpdateAdsClientRequest
{
    public int Id { get; init; }
    public Guid ApiKey { get; set; }
    public string? Name { get; set; }
    public string? WebSite { get; set; }
    public string? ClientInfo { get; init; }
}