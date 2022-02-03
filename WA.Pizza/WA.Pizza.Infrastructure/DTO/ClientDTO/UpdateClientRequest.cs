using System;

namespace WA.Pizza.Infrastructure.DTO.ClientDto;

public class UpdateClientRequest
{
    public int Id { get; set; }
    public Guid ApiToken { get; set; }
    public string Name { get; set; }
    public string WebSite { get; set; }
}