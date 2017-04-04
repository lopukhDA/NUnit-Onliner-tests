using NUnit.Framework;

using AventStack.ExtentReports;

namespace Onliner_tests.Tests
{
    [TestFixture]
    [Parallelizable]
    public class LoginOnlinerTests : BaseTastClass
    {

        [TestCaseSource(typeof(DataForTests), "DataTestAccount")]
        public void SuccessLogin(string login, string pass)
        {
            var loginPage = new PageObject.LoginPage(webDriver);
            loginPage.Open();
            loginPage.Login(login, pass);
            var username = webDriver.WaitForElementIsVisible(loginPage.Username);
            Assert.AreEqual("Dzmitry_Lopukh_test", webDriver.GetText(username), "The page's username is different than expected");
            log.Log(Status.Pass, "Successful login user");
        }

    }

}
