using System;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

namespace WA.Pizza.Infrastructure.DTO.AdsClientDTO;

public class CreateAdsClientRequest
{
    public Guid ApiToken { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
    public ICollection<AdvertisingDto> Advertisings { get; set; }
}