using NUnit.Framework;
using System.Configuration;
using RelevantCodes.ExtentReports;

namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    public class LoginOnlinerTests : BaseTastClass
    {

        [TestCaseSource(typeof(DataForTests), "DataTestAccount")]
        public void SuccessLogin(string login, string pass)
        {
            var loginPage = new PageObject.LoginPage(webDriver, log);
            loginPage.Open();
            loginPage.Login(login, pass);
            var username = webDriver.WaitElement(loginPage.Username);
            Assert.AreEqual("Dzmitry_Lopukh_test", webDriver.GetText(username), "The page's username is different than expected");
            log.Log(LogStatus.Pass, "Successful login user");
        }

    }

    
}
