using System.Net.Http;

public class CustomHttpClientHandler : HttpClientHandler
{
    public CustomHttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
    }
}
