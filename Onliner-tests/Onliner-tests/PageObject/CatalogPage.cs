using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Onliner_tests.PageObject
{
    class CatalogPage
    {
        private WebDriver _driver;

        public CatalogPage(WebDriver driver)
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

        public string[] GetAllDescriptioninThePage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(ProductDescription);
            string[] allDescriptioninText = new string[allElements.Count];
            int i = 0;
            foreach (IWebElement element in allElements)
            {
                String Descriptionin = element.GetAttribute("innerHTML").Replace("&nbsp;", " ");
                allDescriptioninText[i++] = Descriptionin.ToString();
            }
            return allDescriptioninText;
        }

        public List<string> GetListJsonFullName(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Host = "catalog.api.onliner.by";
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
            _driver.WaitWhileElementClassContainsText(LoadingProduct, "schema-products_processing");
        }

        public void WaitProcessing()
        {
            _driver.WaitElement(LoadingProductProcessing);
        }

        public double[] GetAllStarsInThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(RatingStar);
            double[] allStarsText = new double[allElements.Count];
            int i = 0;
            foreach (IWebElement element in allElements)
            {
                String stars = element.GetAttribute("class").Replace("rating", "").Replace(" ", "").Replace("_", "").Replace(",", "");
                allStarsText[i++] = Convert.ToDouble(stars);
            }
            return allStarsText;
        }

        public List<string> GetListPagefullName()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(FullNameProducts);
            List<string> fullNameList = new List<string>();
            foreach (IWebElement element in allElements)
            {
                String fullname = element.GetAttribute("innerHTML").Replace("&quot;", "\"").Replace("&#039;", "'").Replace("&nbsp;", " ");
                fullNameList.Add(fullname);
            }
            return fullNameList;
        }

        public double[] GetAllPriceInThisPage()
        {
            IList<IWebElement> allElements = _driver.FindAllElements(PriceProducts);
            double[] allPriceText = new double[allElements.Count];
            int i = 0;
            foreach (IWebElement element in allElements)
            {
                String price = element.GetAttribute("innerHTML").Replace("&nbsp;", "").Replace("р.", "").Replace(",", ".");
                allPriceText[i++] = Convert.ToDouble(price);
            }
            return allPriceText;
        }

    }
}
