﻿namespace WA.Pizza.Infrastructure.DTO.AdvertisingDTO;

public class UpdateAdvertisingRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public string WebSite { get; set; }
    public int AdsClientId { get; set; }
}