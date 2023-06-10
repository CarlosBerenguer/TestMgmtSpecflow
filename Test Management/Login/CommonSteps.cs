using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestJamaTestMgmt.Config;

namespace TestJamaTestMgmt.Test_Management.Login
{
    [Binding]
    [CollectionDefinition(nameof(AutomationWebFixtureCollection))]
    public class CommonSteps
    {
        private readonly AutomationWebTestFixture _testFixture;
        private readonly LoginScreen _loginScreen;

        public CommonSteps(AutomationWebTestFixture testFixture)
        {
            _testFixture = testFixture;
            _loginScreen = new LoginScreen(testFixture.BrowserHelper);
        }

        [Given(@"the user accessed the login page")]
        public void GivenTheUserAccessedTheLoginPage()
        {
            //Act
            _loginScreen.AccessSystem();

            //Assert
            Assert.Contains(expectedSubstring: _testFixture.Configuration.LoginUrl, actualString: _loginScreen.GetUrl());
        }
        
        [When(@"The user filled the login form")]
        public void WhenTheUserFilledTheLoginForm()
        {
            //Arrange 
            var user = new User{ Username = "sample", Password = "password" };
            _testFixture.User = user;

            //Act 
            _loginScreen.FillLoginForm(user);

            //Assert
            Assert.True(_loginScreen.ValidateFillLoginForm(user));
        }

        [When(@"clicked on Login button")]
        public void WhenClickedOnLoginButton()
        {
            //Arrange 
            //Act 
            _loginScreen.ClickOnLoginButton();
            //Assert
        }


        [Then(@"The user is redirected to the main page")]
        public void ThenTheUserIsRedirectedToTheMainPage()
        {
            Assert.True(_loginScreen.ValidateSuccessLogin());
        }
    }
}
