using System;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.ForumPosts;
using SurfaceServiceServer.Controllers;
using Xunit;

namespace Tests.ControllerUnitTests
{
    public class JsonPlaceholderControllerTests
    {
        [Fact]
        public async Task GetForumPostsByUserAsync_ReturnsList_WhenServiceSucceeds()
        {
            // Arrange
            var userId = 2;
            var expected = new List<ForumPostResponse>
            {
                new ForumPostResponse { UserId = userId, Id = 10, Title = "t1", Body = "b1" }
            };

            var serviceMock = new Mock<IForumPostService>();
            serviceMock
                .Setup(s => s.GetForumPostsByUserAsync(userId))
                .ReturnsAsync(expected);

            var loggerMock = new Mock<ILogger<JsonPlaceholderController>>();
            var controller = new JsonPlaceholderController(loggerMock.Object, serviceMock.Object);

            // Act
            var result = await controller.GetForumPostsByUserAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expected[0].Id, result[0].Id);
            Assert.Equal(expected[0].Title, result[0].Title);

            serviceMock.Verify(s => s.GetForumPostsByUserAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetForumPostsByUserAsync_PropagatesException_WhenServiceThrows()
        {
            // Arrange
            var userId = 2;
            var serviceMock = new Mock<IForumPostService>();
            serviceMock
                .Setup(s => s.GetForumPostsByUserAsync(userId))
                .ThrowsAsync(new InvalidOperationException("service failure"));

            var loggerMock = new Mock<ILogger<JsonPlaceholderController>>();
            var controller = new JsonPlaceholderController(loggerMock.Object, serviceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.GetForumPostsByUserAsync(userId));

            serviceMock.Verify(s => s.GetForumPostsByUserAsync(userId), Times.Once);
        }


        [Fact]
        public async Task CreateForumPostAsync_ReturnsCreatedPost_WhenServiceSucceeds()
        {
            // Arrange
            var forumPostRequest = new ForumPostRequest
            {
                UserId = 1,
                Title = "foo",
                Body = "bar"
            };

            var expectedResponse = new ForumPostResponse
            {
                UserId = 1,
                Id = 101,
                Title = "foo",
                Body = "bar"
            };

            var serviceMock = new Mock<IForumPostService>();
            serviceMock
                .Setup(s => s.CreateForumPostAsync(forumPostRequest))
                .ReturnsAsync(expectedResponse);

            var loggerMock = new Mock<ILogger<JsonPlaceholderController>>();

            var controller = new JsonPlaceholderController(loggerMock.Object, serviceMock.Object);

            // Act
            var result = await controller.CreateForumPostAsync(forumPostRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.UserId, result.UserId);
            Assert.Equal(expectedResponse.Id, result.Id);
            Assert.Equal(expectedResponse.Title, result.Title);
            Assert.Equal(expectedResponse.Body, result.Body);

            serviceMock.Verify(s => s.CreateForumPostAsync(forumPostRequest), Times.Once);
        }

        [Fact]
        public async Task CreateForumPostAsync_PropagatesException_WhenServiceThrows()
        {
            // Arrange
            var forumPostRequest = new ForumPostRequest
            {
                UserId = 1,
                Title = "foo",
                Body = "bar"
            };

            var serviceMock = new Mock<IForumPostService>();
            serviceMock
                .Setup(s => s.CreateForumPostAsync(forumPostRequest))
                .ThrowsAsync(new InvalidOperationException("service failure"));

            var loggerMock = new Mock<ILogger<JsonPlaceholderController>>();

            var controller = new JsonPlaceholderController(loggerMock.Object, serviceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.CreateForumPostAsync(forumPostRequest));

            serviceMock.Verify(s => s.CreateForumPostAsync(forumPostRequest), Times.Once);
        }
    }
}