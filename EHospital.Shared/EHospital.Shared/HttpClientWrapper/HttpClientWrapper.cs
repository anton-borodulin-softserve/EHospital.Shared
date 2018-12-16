using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EHospital.Shared.HttpClientWrapper
{
  public class HttpClientWrapper : HttpClientWrapperBase, IHttpClientWrapper
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// If you want to use HTTPClientWrapper with Factory -
    /// Please add line "services.AddHttpClient<HttpClientWrapper>();" to startup.cs in asp.net project.
    public HttpClientWrapper(HttpClient httpClient) :base(httpClient)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    /// Use this constructor if you don't have possibility to use HttpClientFactory.
    /// 
    // ReSharper disable once RedundantBaseConstructorCall
    public HttpClientWrapper() :base()
    {
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendGetRequest(string requestUri)
    {
      return await SenRequest(HttpMethod.Get, requestUri);
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendGetRequest(string requestUri, string token)
    {
      return await SenRequest(HttpMethod.Get, requestUri, token);
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendPostRequest(string requestUri, string body)
    {
      return await SenRequest(HttpMethod.Post, requestUri, body: body);
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendPostRequest(string requestUri, object body)
    {
      string json = JsonConvert.SerializeObject(body);
      return await SenRequest(HttpMethod.Post, requestUri, body: json);
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendPostRequest(string requestUri, string token, string body)
    {
      return await SenRequest(HttpMethod.Post, requestUri, token, body);
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendPostRequest(string requestUri, string token, object body)
    {
      string json = JsonConvert.SerializeObject(body);
      return await SenRequest(HttpMethod.Post, requestUri, token, json);
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
    {
      return await SendRequest(request);
    }
  }
}
