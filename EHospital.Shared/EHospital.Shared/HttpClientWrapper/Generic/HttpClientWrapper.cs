using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EHospital.Shared.HttpClientWrapper.Generic
{
  public class HttpClientWrapper<Result> : HttpClientWrapperBase, IHttpClientWrapper<Result>
    where Result : class
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// If you want to use HTTPClientWrapper with Factory -
    /// Please add line "services.AddHttpClient<HttpClientWrapper>();" to startup.cs in asp.net project.
    public HttpClientWrapper(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
    /// </summary>
    /// Use this constructor if you don't have possibility to use HttpClientFactory.
    /// 
    // ReSharper disable once RedundantBaseConstructorCall
    public HttpClientWrapper() : base()
    {
    }

    /// <inheritdoc />
    public async Task<Result> SendRequest(string requestUri, HttpMethod httpMethod, object body, string token = "")
    {
      string json = JsonConvert.SerializeObject(body);

      HttpResponseMessage responseMessage = await SenRequest(HttpMethod.Post, requestUri, token, json);

      string responseContent = await responseMessage.Content.ReadAsStringAsync();

      return JsonConvert.DeserializeObject<Result>(responseContent);
    }
  }
}
