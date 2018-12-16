using System.Net.Http;
using System.Threading.Tasks;

namespace EHospital.Shared.HttpClientWrapper
{
  public interface IHttpClientWrapper
  {
    /// <summary>
    /// Sends the get request.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <returns>The response message.</returns>
    Task<HttpResponseMessage> SendGetRequest(string requestUri);

    /// <summary>
    /// Sends the get request.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="token">The token.</param>
    /// <returns>The response message.</returns>
    Task<HttpResponseMessage> SendGetRequest(string requestUri, string token);

    /// <summary>
    /// Sends the post request.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="body">The body.</param>
    /// <returns>The response message.</returns>
    Task<HttpResponseMessage> SendPostRequest(string requestUri, string body);

    /// <summary>
    /// Sends the post request.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="body">The body.</param>
    /// <returns>The response message.</returns>
    Task<HttpResponseMessage> SendPostRequest(string requestUri, object body);

    /// <summary>
    /// Sends the post request.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="token">The token.</param>
    /// <param name="body">The body.</param>
    /// <returns>The response message.</returns>
    Task<HttpResponseMessage> SendPostRequest(string requestUri, string token, string body);

    /// <summary>
    /// Sends the post request.
    /// </summary>
    /// <param name="requestUri">The request URI.</param>
    /// <param name="token">The token.</param>
    /// <param name="body">The body.</param>
    /// <returns>The response message.</returns>
    Task<HttpResponseMessage> SendPostRequest(string requestUri, string token, object body);

    /// <summary>
    /// Sends the request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The response message.</returns>
    Task<HttpResponseMessage> SendRequest(HttpRequestMessage request);
  }
}
