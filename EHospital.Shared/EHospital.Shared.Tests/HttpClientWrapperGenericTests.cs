using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EHospital.Shared.HttpClientWrapper.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace EHospital.Shared.Tests
{
  [TestClass]
  public class HttpClientWrapperGenericTests
  {
    private IHttpClientWrapper<ResponseExample[]> httpClientWrapper;

    [TestMethod]
    public async Task SendPostRequest_Success()
    {
      //Arrange
      var requestUri = "https://localhost:44393/api/Allergies";
      var messageBody = new { pathogen = "Pathogen" };
      var httpMethod = HttpMethod.Post;
      httpClientWrapper = new HttpClientWrapper<ResponseExample[]>(GenerateHttpClient(httpMethod, requestUri, messageBody));


      //Act
      var message = await httpClientWrapper.SendRequest(requestUri, httpMethod, messageBody);

      //Assert
      Assert.IsNotNull(message);
      Assert.AreEqual(message.Length, 1);
      Assert.AreEqual(message[0].Pathogen, messageBody.pathogen);
    }

    private HttpClient GenerateHttpClient(HttpMethod httpMethod, string requestUri, object body = null)
    {
      var messageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

      var responseArray = new[] { new ResponseExample { Id = 1, Pathogen = "Pathogen" } };

      messageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
          ItExpr.IsAny<CancellationToken>())

        .Returns(Task.Run(() =>
        {
          var response = new HttpResponseMessage(HttpStatusCode.OK)
          {
            Content = new StringContent(JsonConvert.SerializeObject(responseArray))
          };

          return response;

        }))

        .Callback<HttpRequestMessage, CancellationToken>((r, c) =>
        {

          Assert.AreEqual(httpMethod, r.Method);
          Assert.AreEqual(requestUri, r.RequestUri.ToString());

          if (body != null)
          {
            Assert.AreEqual(JsonConvert.SerializeObject(body), r.Content.ReadAsStringAsync().Result);
          }

        });

      return new HttpClient(messageHandler.Object);
    }
  }
}
