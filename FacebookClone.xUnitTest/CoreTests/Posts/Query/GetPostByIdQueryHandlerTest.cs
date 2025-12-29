using FacebookClone.Core.Feature.Post.Queries.Handlers;
using FacebookClone.Core.Feature.Post.Queries.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Service.Abstract;
using FluentAssertions;
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
        [Theory]
        [InlineData(1)]
        public async Task Handle_ValidPostId_ReturnsPostDto(int id)
        {
            // Arrange
            var expectedPost = new PostDto
            {
                PostId = id,
                Content = "iam yousef",
                Privacy = "public",
                ParentPostId = null,
                LikeCount = 4,
                CommentCount = 0
            };

            _postServiceMock
                .Setup(x => x.GetPostById(id))
                .ReturnsAsync(expectedPost);

            var handler = new GetPostByIdQueryHandler(_postServiceMock.Object);

            // Act
            var result = await handler.Handle(
                new GetPostByIdQuery { PostId = id },
                CancellationToken.None
            );

            // Assert
            result.Should().NotBeNull();
            result.PostId.Should().Be(id);
            result.Content.Should().Be("iam yousef");
        }
        [Theory]
        [InlineData(99)]
        public async Task Handle_InvalidPostId_ReturnsNull(int id)
        {
            _postServiceMock
                .Setup(x => x.GetPostById(id))
                .ReturnsAsync((PostDto)null);

            var handler = new GetPostByIdQueryHandler(_postServiceMock.Object);

            var result = await handler.Handle(
                new GetPostByIdQuery { PostId = id },
                CancellationToken.None
            );

            result.Should().BeNull();
        }




    }
}
