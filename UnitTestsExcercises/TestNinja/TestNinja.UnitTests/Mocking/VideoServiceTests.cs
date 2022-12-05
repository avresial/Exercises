using Moq;
using System;
using System.Collections.Generic;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class VideoServiceTests
    {
        [Fact]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            Mock<IFileReader> fileReader = new Mock<IFileReader>();
            fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            VideoService videoService = new VideoService(fileReader.Object, null);

            var result = videoService.ReadVideoTitle();

            Assert.Contains("error", result, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_SingleVideo_ReturnsStringWitkNoSeparator()
        {
            Mock<IVideoRepository> VideoRepository = new Mock<IVideoRepository>();

            List<Video> videos = new List<Video>() { new Video() { Id = 1, Title = "test", IsProcessed = true } };
            VideoRepository.Setup(fr => fr.GetUnprocessedVideos()).Returns(videos);

            VideoService videoService = new VideoService(null, VideoRepository.Object);


            var result = videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal("1", result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_TwoVideos_ReturnsStringWitkSeparator()
        {
            Mock<IVideoRepository> VideoRepository = new Mock<IVideoRepository>();

            List<Video> videos = new List<Video>() {
                new Video() { Id = 1, Title = "test", IsProcessed = true },
                new Video() { Id = 2, Title = "test", IsProcessed = true }
            };

            VideoRepository.Setup(fr => fr.GetUnprocessedVideos()).Returns(videos);
            VideoService videoService = new VideoService(null, VideoRepository.Object);

            var result = videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal("1,2", result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_ZeroVideos_ReturnsEmptyString()
        {
            Mock<IVideoRepository> VideoRepository = new Mock<IVideoRepository>();

            List<Video> videos = new List<Video>();
            VideoRepository.Setup(fr => fr.GetUnprocessedVideos()).Returns(videos);
            VideoService videoService = new VideoService(null, VideoRepository.Object);


            var result = videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_VideoContextIsNull_ReturnsException()
        {
            VideoService videoService = new VideoService(null,null);

            Exception ex = Record.Exception(() => videoService.GetUnprocessedVideosAsCsv());

            Assert.IsType<NullReferenceException>(ex);
        }
    }
}
