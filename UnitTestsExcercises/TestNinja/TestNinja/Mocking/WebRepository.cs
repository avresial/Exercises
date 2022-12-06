using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
