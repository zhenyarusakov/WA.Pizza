﻿using System;

namespace WA.Pizza.Infrastructure.DTO.ClientDto;

public class ClientDto
{
    public int Id { get; set; }
    public Guid ApiToken { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
}