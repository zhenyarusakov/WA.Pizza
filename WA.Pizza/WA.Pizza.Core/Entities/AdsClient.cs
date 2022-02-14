using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities;

public class AdsClient
{
    public int Id { get; init; }
    public Guid ApiKey { get; set; }
    public string? Name { get; init; }
    public string? WebSite { get; init; }
    public string? ClientInfo { get; init; }
    public bool IsBlocked { get; set; }
    public ICollection<Advertisement> Advertisements { get; } = new List<Advertisement>();
}