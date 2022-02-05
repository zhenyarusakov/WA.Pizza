using System;
using System.Collections.Generic;

namespace WA.Pizza.Core.Entities;

public class AdsClient
{
    public int Id { get; set; }
    public Guid ApiKey { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
    public ICollection<Advertising> Advertisings { get; set; } = new List<Advertising>();
}