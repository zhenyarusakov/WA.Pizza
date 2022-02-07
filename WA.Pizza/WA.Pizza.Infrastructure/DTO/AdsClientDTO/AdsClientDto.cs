﻿using System;
using System.Collections.Generic;
using WA.Pizza.Infrastructure.DTO.AdvertisementDTO;

namespace WA.Pizza.Infrastructure.DTO.AdsClientDTO;

public class AdsClientDto
{
    public int Id { get; set; }
    public Guid ApiToken { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
    public ICollection<AdvertisementDto> Advertisements { get; set; }
}