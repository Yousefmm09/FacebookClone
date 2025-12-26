using FacebookClone.Core.Feature.Post.Queries.Handlers;
using FacebookClone.Core.Feature.Post.Queries.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Service.Abstract;
using Moq;

namespace FacebookClone.xUnitTest.CoreTests.Posts.Query
{
    public class GetPostByIdQueryHandlerTest
    {
        private readonly Mock<IPostService> _postServiceMock;
        public GetPostByIdQueryHandlerTest()
        {
            _postServiceMock = new Mock<IPostService>();
        }
        [Fact]
        public async Task Handle_ValidPostId_ReturnsPostDto()
        {
            // Arrange
            var postEntity = new PostDto
            {
                PostId = 23,
                Content = "This is a sample post.",
                Privacy = "Public",
                LikeCount = 10
            };

            var query = new GetPostByIdQuery
            {
                PostId = 23
            };

            _postServiceMock.Setup(x => x.GetPostById(23)).ReturnsAsync(postEntity);

            var handler = new GetPostByIdQueryHandler(_postServiceMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostDto>(result);
            Assert.Equal(23, result.PostId);
            Assert.Equal("This is a sample post.", result.Content);
            Assert.Equal("Public", result.Privacy);
            Assert.Equal(10, result.LikeCount);

            _postServiceMock.Verify(x => x.GetPostById(23), Times.Once);
        }


    }
}
