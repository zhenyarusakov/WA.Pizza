using System;

namespace WA.Pizza.Core.Entities;

public class AdsClient
{
    public int Id { get; set; }
    public Guid ApiToken { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
}