using FacebookClone.Core.Feature.Post.Command.Handlers;
using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Service.Abstract;
using Moq;

namespace FacebookClone.xUnitTest.CoreTests.Posts.Commands
{
    public class CreatPostCommandHandlerTest
    {
        private readonly Mock<IPostService> _postServiceMock;
        public CreatPostCommandHandlerTest()
        {
            _postServiceMock = new Mock<IPostService>();
        }
        [Fact]
        public async Task Handle_ValidCommand_ReturnsCreatedPostDto()
        {
            // Arrange
            var command = new CreatPostCommand
            {
                ParentPostId = null,
                Content = "hi",
                Privacy = "Public"
            };

            var postEntityReturnedFromService = new PostDto
            {
                PostId = 1,
                Content = "hi",
                Privacy = "Public"
            };

            _postServiceMock
                .Setup(x => x.CreatPostAsync(It.IsAny<PostDto>()))
                .ReturnsAsync(postEntityReturnedFromService);

            var handler = new CreatPostCommandHandler(_postServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.PostId);
            Assert.Equal("hi", result.Content);
            Assert.Equal("Public", result.Privacy);

            _postServiceMock.Verify(
                x => x.CreatPostAsync(It.IsAny<PostDto>()),
                Times.Once
            );
        }

    }
}
