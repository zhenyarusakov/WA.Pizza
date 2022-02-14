using System;

namespace WA.Pizza.Infrastructure.DTO.AdsClientDTO;

public class CreateAdsClientRequest
{
    public Guid ApiKey { get; set; }
    public string? Name { get; init; }
    public string? WebSite { get; init; }
    public bool IsBlocked { get; init; }
    public string? ClientInfo { get; set; }
}