using System.Collections.Generic;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

public class AdvertisingHelper
{
    public static ICollection<Advertising> CreateListOfFilledAdvertising()
    {
        List<Advertising> advertisings = new List<Advertising>();
        
        advertisings.Add(new Advertising
        {
            Id = 1,
            Name = "pepsi",
            Description = "pepsi",
            Img = "pepsi.png",
            WebSite = "pepsi.com"
        });
        
        advertisings.Add(new Advertising
        {
            Id = 2,
            Name = "coca-cola",
            Description = "coca-cola",
            Img = "coca-cola.png",
            WebSite = "coca-cola.com"
        });

        return advertisings;
    }
}