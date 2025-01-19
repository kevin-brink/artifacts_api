// using System.Net;
// using System.Net.Http;
// using System.Threading;
// using System.Threading.Tasks;
// using Moq;
// using Moq.Protected;
// using Xunit;

// namespace ArtifactsAPI.Tests
// {
//     public class ActionEndpointsTests
//     {
//         [Fact]
//         public async Task Move_SendsCorrectRequest()
//         {
//             // Arrange
//             var handlerMock = new Mock<HttpMessageHandler>();
//             handlerMock
//                 .Protected()
//                 .Setup<Task<HttpResponseMessage>>(
//                     "SendAsync",
//                     ItExpr.Is<HttpRequestMessage>(req =>
//                         req.Method == HttpMethod.Post &&
//                         req.RequestUri == new Uri("http://example.com/action/move") &&
//                         req.Content.ReadAsStringAsync().Result == "{\"x\":0,\"y\":1}"
//                     ),
//                     ItExpr.IsAny<CancellationToken>()
//                 )
//                 .ReturnsAsync(new HttpResponseMessage
//                 {
//                     StatusCode = HttpStatusCode.OK,
//                     Content = new StringContent("{\"status\":\"success\"}"),
//                 });

//             var httpClient = new HttpClient(handlerMock.Object)
//             {
//                 BaseAddress = new Uri("http://example.com/")
//             };

//             var apiHandler = new APIHandler("api_key", "name")
//             {
//                 _client = httpClient // Assuming _client is accessible for testing purposes
//             };

//             var actionEndpoints = new APIHandler.ActionEndpoints(apiHandler);

//             // Act
//             var response = await actionEndpoints.Move(0, 1);

//             // Assert
//             Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//             handlerMock.Protected().Verify(
//                 "SendAsync",
//                 Times.Once(),
//                 ItExpr.Is<HttpRequestMessage>(req =>
//                     req.Method == HttpMethod.Post &&
//                     req.RequestUri == new Uri("http://example.com/action/move") &&
//                     req.Content.ReadAsStringAsync().Result == "{\"x\":0,\"y\":1}"
//                 ),
//                 ItExpr.IsAny<CancellationToken>()
//             );
//         }
//     }
// }

// TODO Make unit tests and get them working...
