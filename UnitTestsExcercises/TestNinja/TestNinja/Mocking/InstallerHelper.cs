using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private IWebRepository webRepository;

        public string _setupDestinationFile;
        
        public InstallerHelper(IWebRepository webRepository)
        {
            this.webRepository = webRepository ?? new WebRepository();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                string address = string.Format("http://example.com/{0}/{1}", customerName, installerName);

                webRepository.DownloadFile(address, _setupDestinationFile);
                
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}