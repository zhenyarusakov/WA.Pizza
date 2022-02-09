using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities;

public class AdsClient
{
    public int Id { get; set; }
    public Guid ApiKey { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
    public string ClientInfo { get; set; }
    public bool IsBlocked { get; set; }
    public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
}