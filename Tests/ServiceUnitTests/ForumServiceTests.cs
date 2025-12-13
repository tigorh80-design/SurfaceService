using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer;
using Moq;
using Moq.Protected;
using Shared.ForumPosts;
using Xunit;

namespace Tests.ServiceUnitTests
{
    public class ForumServiceTests
    {
        [Fact]
        public async Task GetForumPostsByUserAsync_ReturnsPosts_WhenApiReturnsSuccess()
        {
            // Arrange
            var userId = 2;
            var responseJson = "[{\"userId\":2,\"id\":5,\"title\":\"t1\",\"body\":\"b1\"}]";

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            HttpRequestMessage? capturedRequest = null;

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage req, CancellationToken ct) =>
                {
                    capturedRequest = req;
                    return response;
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(f => f.CreateClient("JSONPlaceholder")).Returns(httpClient);

            var service = new ForumPostService(factoryMock.Object);

            // Act
            var result = await service.GetForumPostsByUserAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(2, result[0].UserId);
            Assert.Equal(5, result[0].Id);
            Assert.Equal("t1", result[0].Title);
            Assert.Equal("b1", result[0].Body);

            Assert.NotNull(capturedRequest);
            Assert.Equal(HttpMethod.Get, capturedRequest!.Method);
            Assert.EndsWith($"/users/{userId}/posts", capturedRequest.RequestUri!.AbsolutePath, StringComparison.OrdinalIgnoreCase);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetForumPostsByUserAsync_ThrowsHttpRequestException_WhenApiReturnsError()
        {
            // Arrange
            var userId = 2;

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("{\"error\":\"server\"}", System.Text.Encoding.UTF8, "application/json")
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(f => f.CreateClient("JSONPlaceholder")).Returns(httpClient);

            var service = new ForumPostService(factoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => service.GetForumPostsByUserAsync(userId));

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task CreateForumPostAsync_ReturnsCreatedPost_WhenApiReturnsSuccess()
        {
            // Arrange
            var requestPayload = new ForumPostRequest { UserId = 1, Title = "foo", Body = "bar" };
            var responseJson = "{\"userId\":1,\"id\":101,\"title\":\"foo\",\"body\":\"bar\"}";

            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
            };

            HttpRequestMessage? capturedRequest = null;

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage req, CancellationToken ct) =>
                {
                    capturedRequest = req;
                    return response;
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(f => f.CreateClient("JSONPlaceholder")).Returns(httpClient);

            var service = new ForumPostService(factoryMock.Object);

            // Act
            var result = await service.CreateForumPostAsync(requestPayload);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal(101, result.Id);
            Assert.Equal("foo", result.Title);
            Assert.Equal("bar", result.Body);

            Assert.NotNull(capturedRequest);
            Assert.Equal(HttpMethod.Post, capturedRequest!.Method);
            Assert.EndsWith("posts", capturedRequest.RequestUri!.AbsolutePath, StringComparison.OrdinalIgnoreCase);

            var content = await capturedRequest.Content!.ReadAsStringAsync();
            Assert.Contains("\"title\":\"foo\"", content);
            Assert.Contains("\"body\":\"bar\"", content);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task CreateForumPostAsync_ThrowsHttpRequestException_WhenApiReturnsError()
        {
            // Arrange
            var requestPayload = new ForumPostRequest { UserId = 1, Title = "foo", Body = "bar" };

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("{\"error\":\"server\"}", System.Text.Encoding.UTF8, "application/json")
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(f => f.CreateClient("JSONPlaceholder")).Returns(httpClient);

            var service = new ForumPostService(factoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => service.CreateForumPostAsync(requestPayload));

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}