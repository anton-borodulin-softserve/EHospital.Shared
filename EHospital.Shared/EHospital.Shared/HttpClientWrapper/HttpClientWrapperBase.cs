using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EHospital.Shared.HttpClientWrapper
{
  public class HttpClientWrapperBase
  {
    private const string URI_IS_EMPTY_ERROR_MESSAGE = "RequestUri shouldn`t be empty.";
    private const string DEFAULT_CONTENT_TYPE = "application/json";

    private static HttpClient _staticHttpClient;

    private readonly HttpClient _httpClient;
    private readonly bool _isFactoryImplementation;


    /// <summary>
    /// If factory approach is used, use factory generated httpclient object, if not use static.
    /// </summary>
    /// <value>
    /// The HTTP client.
    /// </value>
    private HttpClient HttpClient => _isFactoryImplementation ? _httpClient : _staticHttpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// If you want to use HTTPClientWrapper with Factory -
    /// Please add line "services.AddHttpClient<HttpClientWrapper>();" to startup.cs in asp.net project.
    public HttpClientWrapperBase(HttpClient httpClient)
    {
      _isFactoryImplementation = true;
      _httpClient = httpClient;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    /// Use this constructor if you don't have possibility to use HttpClientFactory.
    /// 
    public HttpClientWrapperBase()
    {
      _staticHttpClient = new HttpClient();
    }

    protected async Task<HttpResponseMessage> SenRequest(HttpRequestMessage requestMessage)
    {
      return await HttpClient.SendAsync(requestMessage);
    }

    protected async Task<HttpResponseMessage> SenRequest(HttpMethod method, string requestUri,
      string token = "",
      string body = "",
      string contentType = DEFAULT_CONTENT_TYPE)
    {
      if (string.IsNullOrEmpty(requestUri))
      {
        throw new ArgumentException(URI_IS_EMPTY_ERROR_MESSAGE);
      }

      var httpRequest = new HttpRequestMessage(method, requestUri);

      HttpClient.DefaultRequestHeaders
        .Accept
        .Add(new MediaTypeWithQualityHeaderValue(contentType));

      if (!string.IsNullOrEmpty(token))
      {
        httpRequest.Headers.Add("Authorization", token);
      }

      if (!string.IsNullOrEmpty(body))
      {
        httpRequest.Content = new StringContent(body, Encoding.UTF8, contentType);
      }

      return await HttpClient.SendAsync(httpRequest);
    }
  }
}
