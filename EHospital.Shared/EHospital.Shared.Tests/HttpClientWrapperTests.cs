using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EHospital.Shared.HttpClientWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace EHospital.Shared.Tests
{
  [TestClass]
  public class HttpClientWrapperTests
  {
    private IHttpClientWrapper httpClientWrapper;

    [TestMethod]
    public async Task SendGetRequest_Success()
    {
      //Arrange
      var someTestUri = "https://localhost:44393/api/Allergies";
      httpClientWrapper = new HttpClientWrapper.HttpClientWrapper(GenerateHttpClient(HttpMethod.Get, someTestUri));

      //Act
      var message = await httpClientWrapper.SendGetRequest(someTestUri);

      //Assert
      var content = await message.Content.ReadAsStringAsync();
      Assert.IsNotNull(content);
    }

    [TestMethod]
    public async Task SendPostRequest_Success()
    {
      //Arrange
      var someTestUri = "https://localhost:44393/api/Allergies";
      var messageBody = "{\"pathogen\": \"Pathogen\"}";
      httpClientWrapper = new HttpClientWrapper.HttpClientWrapper(GenerateHttpClient(HttpMethod.Post, someTestUri));

      //Act
      var message = await httpClientWrapper.SendPostRequest(someTestUri, messageBody);

      //Assert
      var content = await message.Content.ReadAsStringAsync();
      Assert.IsNotNull(content);
    }

    [TestMethod]
    public async Task SendPostWithObjectRequest_Success()
    {
      //Arrange
      var someTestUri = "https://localhost:44393/api/Allergies";
      var messageBody = new { pathogen = "Pathogen" };
      httpClientWrapper = new HttpClientWrapper.HttpClientWrapper(GenerateHttpClient(HttpMethod.Post, someTestUri, messageBody));

      //Act
      var message = await httpClientWrapper.SendPostRequest(someTestUri, messageBody);

      //Assert
      var content = await message.Content.ReadAsStringAsync();
      Assert.IsNotNull(content);
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
