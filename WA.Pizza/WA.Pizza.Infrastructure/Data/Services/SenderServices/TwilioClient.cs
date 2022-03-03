using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio.Clients;
using Twilio.Http;

namespace WA.Pizza.Infrastructure.Data.Services.SenderServices;

public class TwilioClient: ITwilioRestClient
{
    private readonly ITwilioRestClient _client;

    public TwilioClient(IConfiguration config, System.Net.Http.HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "CustomTwilioRestClient-Demo");
        _client = new TwilioRestClient(
            config["Twilio:AccountSid"],
            config["Twilio:AuthToken"],
            httpClient: new SystemNetHttpClient(httpClient));
    }
    
    public Response Request(Request request)
    {
        return _client.Request(request);
    }

    public Task<Response> RequestAsync(Request request)
    {
        return _client.RequestAsync(request);
    }

    public string AccountSid => _client.AccountSid;
    public string Region => _client.Region;
    public HttpClient HttpClient => _client.HttpClient;
}