using FacebookClone.Core.Feature.Post.Command.Handlers;
using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Service.Abstract;
using Moq;

namespace FacebookClone.xUnitTest.CoreTests.Posts.Commands
{
    public class DeletePostCommandTest
    {
        private readonly Mock<IPostService> _postServiceMock;
        public DeletePostCommandTest()
        {
            _postServiceMock = new Mock<IPostService>();
        }
        [Theory]
        [InlineData(42)]
        public async Task Handle_ValidPostId_ReturnsSuccessMessage(int postId)
        {
            // Arrange
            var command = new DeletePostCommand
            {
                PostId = postId
            };

            _postServiceMock
                .Setup(x => x.DeletePost(postId))
                .ReturnsAsync("the post deleted successfully");

            var handler = new DeletePostCommandHandler(_postServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("the post deleted successfully", result);

            _postServiceMock.Verify(
                x => x.DeletePost(postId),
                Times.Once
            );
        }


    }
}
