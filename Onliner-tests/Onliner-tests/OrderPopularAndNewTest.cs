using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Onliner_tests
{
    [TestFixture]
    [Parallelizable]
    class OrderPopularAndNewTest : BaseTastClass
    {
        [Test]
        public void newResponce()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://catalog.api.onliner.by/search/notebook?group=0&order=date:desc");
            request.Method = "GET";
            request.Host = "catalog.api.onliner.by";
            request.Accept = "application/json, text/javascript, */*; q=0.01";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());

            response.Close();
            Assert.Pass();
        }
    }
}
