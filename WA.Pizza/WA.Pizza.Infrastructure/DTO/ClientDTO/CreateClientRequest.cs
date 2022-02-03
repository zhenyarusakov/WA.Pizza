using System;

namespace WA.Pizza.Infrastructure.DTO.ClientDto;

public class CreateClientRequest
{
    public Guid ApiToken { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
}