using Moq;
using System;
using System.Collections.Generic;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class VideoServiceTests
    {
        Mock<IFileReader> fileReader;
        Mock<IVideoRepository> videoRepository;
        VideoService videoService;

        public VideoServiceTests()
        {
            this.fileReader = new Mock<IFileReader>();
            this.videoRepository = new Mock<IVideoRepository>();
            videoService = new VideoService(this.fileReader.Object, this.videoRepository.Object);
        }

        [Fact]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            fileReader.Setup(r => r.Read("video.txt")).Returns("");

            string result = videoService.ReadVideoTitle();

            Assert.Contains("error", result, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_SingleUnprocessedVideo_ReturnsStringWitkNoSeparator()
        {
            List<Video> videos = new List<Video>() { new Video() { Id = 1 } };
            videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(videos);

            string result = videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal("1", result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_TwoUnprocessedVideos_ReturnsStringWitkSeparator()
        {
            List<Video> videos = new List<Video>() {
                new Video() { Id = 1},
                new Video() { Id = 2}
            };

            videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(videos);

            string result = videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal("1,2", result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
        {
            List<Video> videos = new List<Video>();
            videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(videos);

            string result = videoService.GetUnprocessedVideosAsCsv();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_VideoContextIsNull_ReturnsException()
        {
            VideoService videoService = new VideoService(null, null);

            Exception ex = Record.Exception(() => videoService.GetUnprocessedVideosAsCsv());

            Assert.IsType<NullReferenceException>(ex);
        }
    }
}
