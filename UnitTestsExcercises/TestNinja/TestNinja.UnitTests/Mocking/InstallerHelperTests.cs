using Moq;
using System.Net;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class InstallerHelperTests
    {
        private Mock<IWebRepository> WebRepository;
        private InstallerHelper installerHelper;
        public InstallerHelperTests()
        {
            WebRepository = new Mock<IWebRepository>();
            installerHelper = new InstallerHelper(WebRepository.Object);
        }

        [Fact]
        public void DownloadInstaller_DownloadCompletes_ReturnsTrue()
        {
            var result = installerHelper.DownloadInstaller("customerName", "installerName");

            Assert.True(result);
        }

        [Fact]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            WebRepository.Setup(x => x.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws(new WebException());

            var result = installerHelper.DownloadInstaller("customerName", "installerName");

            Assert.False(result);
        }
    }
}
