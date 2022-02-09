using System.Collections.Generic;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Infrastructure.Tests.Infrastructure.Helpers;

public class AdvertisementHelper
{
    public static ICollection<Advertisement> CreateListOfFilledAdvertising()
    {
        List<Advertisement> advertisements = new List<Advertisement>();
        
        advertisements.Add(new Advertisement
        {
            Id = 1,
            Name = "pepsi",
            Description = "pepsi",
            Img = "pepsi.png",
            WebSite = "pepsi.com",
            AdsClientId = 1
        });
        
        advertisements.Add(new Advertisement
        {
            Id = 2,
            Name = "pepsi",
            Description = "pepsi",
            Img = "pepsi.png",
            WebSite = "pepsi.com",
            AdsClientId = 1,
        });

        return advertisements;
    }
}