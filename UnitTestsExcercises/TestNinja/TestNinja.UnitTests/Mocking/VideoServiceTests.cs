using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

            VideoService videoService = new VideoService(fileReader.Object);

            var result = videoService.ReadVideoTitle();

            Assert.Contains("error", result, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_SingleVideo_ReturnsStringWitkNoSeparator()
        {
            Mock<IFileReader> fileReader = new Mock<IFileReader>();
            fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            Mock<IVideoContext> VideoContext = new Mock<IVideoContext>();

            List<Video> videos = new List<Video>() { new Video() { Id = 1, Title = "test", IsProcessed = true } };
            VideoContext.Setup(fr => fr.GetVideos()).Returns(videos);
            VideoService videoService = new VideoService(fileReader.Object);


            var result = videoService.GetUnprocessedVideosAsCsv(VideoContext.Object);

            Assert.Equal("1", result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_TwoVideos_ReturnsStringWitkSeparator()
        {
            Mock<IFileReader> fileReader = new Mock<IFileReader>();
            fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            Mock<IVideoContext> VideoContext = new Mock<IVideoContext>();

            List<Video> videos = new List<Video>() {
                new Video() { Id = 1, Title = "test", IsProcessed = true },
                new Video() { Id = 2, Title = "test", IsProcessed = true }
            };
            VideoContext.Setup(fr => fr.GetVideos()).Returns(videos);
            VideoService videoService = new VideoService(fileReader.Object);


            var result = videoService.GetUnprocessedVideosAsCsv(VideoContext.Object);

            Assert.Equal("1,2", result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_ZeroVideos_ReturnsEmptyString()
        {
            Mock<IFileReader> fileReader = new Mock<IFileReader>();
            fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            Mock<IVideoContext> VideoContext = new Mock<IVideoContext>();

            List<Video> videos = new List<Video>();
            VideoContext.Setup(fr => fr.GetVideos()).Returns(videos);
            VideoService videoService = new VideoService(fileReader.Object);


            var result = videoService.GetUnprocessedVideosAsCsv(VideoContext.Object);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_VideoContextIsNull_ReturnsException()
        {
            Mock<IFileReader> fileReader = new Mock<IFileReader>();
            fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            Mock<IVideoContext> VideoContext = new Mock<IVideoContext>();

            VideoService videoService = new VideoService(fileReader.Object);

            Exception ex = Record.Exception(() => videoService.GetUnprocessedVideosAsCsv(null));

            Assert.IsType<NullReferenceException>(ex);
        }
    }
}
