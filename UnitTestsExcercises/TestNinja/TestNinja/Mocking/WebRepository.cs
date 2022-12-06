using System.Net;

namespace TestNinja.Mocking
{
    public class WebRepository : IWebRepository
    {
        WebClient client = new WebClient();
        public void DownloadFile(string address, string fileName)
        {
            client.DownloadFile(address, fileName);
        }
    }
}
