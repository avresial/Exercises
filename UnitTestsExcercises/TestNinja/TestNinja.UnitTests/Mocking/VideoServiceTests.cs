using Moq;
using System;
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

    }
}
