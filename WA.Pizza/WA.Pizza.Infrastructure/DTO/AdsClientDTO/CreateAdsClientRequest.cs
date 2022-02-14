using System;

namespace WA.Pizza.Infrastructure.DTO.AdsClientDTO;

public class CreateAdsClientRequest
{
    public Guid ApiKey { get; set; }
    public string? Name { get; set; }
    public string? WebSite { get; set; }
    public bool IsBlocked { get; set; }
    public string? ClientInfo { get; set; }
}