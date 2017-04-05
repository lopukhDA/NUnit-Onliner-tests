using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Onliner_tests.PageObject
{
    class ResultComponent
    {
        private WebDriver _driver;

        public ResultComponent(WebDriver driver)
        {
            _driver = driver;
        }

        public void Open(string url)
        {
            _driver.Navigate(url);
        }

        public By PriceProducts { get; set; } = By.CssSelector(".schema-product__price-value.schema-product__price-value_primary span");
        public By ProductCatalog { get; set; } = By.Id("schema-products");
        public By LoadingProduct { get; set; } = By.CssSelector(".schema-products");
        public By LoadingProductProcessing { get; set; } = By.CssSelector(".schema-products.schema-products_processing");
        public By RatingStar { get; set; } = By.CssSelector(".rating");
        public By FullNameProducts { get; set; } = By.XPath("//span[ contains(@data-bind,'product.extended_name')]");
        public By ProductDescription { get; set; } = By.XPath("//span[ contains(@data-bind,'html: product.description')]");

        public string[] GetAllDescriptionOnThePage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(ProductDescription);
            return allElements.Select(el => el.GetAttribute("innerHTML").Replace("&nbsp;", " ")).ToArray();
        }

        public List<string> GetListJsonFullName(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json, text/javascript, */*; q=0.01";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            reader.Close();
            response.Close();
            var json = JObject.Parse(output.ToString());
            var array = json["products"];
            string[] fullNameArray = new string[5];
            List<string> fullNameList = new List<string>(); ;
            foreach (var item in array)
            {
                if (item["extended_name"].ToString() != null && item["extended_name"].ToString() != "")
                {
                    fullNameList.Add(item["extended_name"].ToString().Replace("&quot;", "\"").Replace("&#039;", "'").Replace("&nbsp;", " "));
                }
                else
                {
                    fullNameList.Add(item["full_name"].ToString().Replace("&quot;", "\"").Replace("&#039;", "'").Replace("&nbsp;", " "));
                }
            }
            return fullNameList;
        }

        public void ProcessingComplite()
        {
            _driver.WaitWhileElementNotClassContainsText(LoadingProduct, "schema-products_processing");
        }

        public void WaitProcessing()
        {
            _driver.WaitForElementIsVisible(LoadingProductProcessing);
        }

        public double[] GetAllStarsOnThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(RatingStar);
            return allElements.Select(el => Convert.ToDouble(el.GetAttribute("class").Replace("rating", "").Replace(" ", "").Replace("_", "").Replace(",", ""))).ToArray();
        }

        public List<string> GetListFullnameOnThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(FullNameProducts);
            return allElements.Select(el => el.GetAttribute("innerHTML").Replace("&quot;", "\"").Replace("&#039;", "'").Replace("&nbsp;", " ")).ToList<string>();
        }

        public double[] GetAllPriceOnThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(PriceProducts);
            return allElements.Select(el => Convert.ToDouble(el.GetAttribute("innerHTML").Replace("&nbsp;", "").Replace("р.", "").Replace(",", "."))).ToArray();
        }

    }
}
