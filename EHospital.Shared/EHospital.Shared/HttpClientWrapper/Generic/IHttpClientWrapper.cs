using System.Net.Http;
using System.Threading.Tasks;

namespace EHospital.Shared.HttpClientWrapper.Generic
{
  public interface IHttpClientWrapper<Result> where Result : class
  {
    /// <summary>
    /// Sends the request.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <param name="body">The body.</param>
    /// <param name="token">The token.</param>
    /// <returns>The response model.</returns>
    Task<Result> SendRequest(string requestUri, HttpMethod httpMethod, object body, string token = "");
  }
}
