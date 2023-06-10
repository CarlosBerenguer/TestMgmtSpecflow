using System;
using TechTalk.SpecFlow;
using TestJamaTestMgmt.Config;
using TestJamaTestMgmt.Test_Management.Login;

namespace TestJamaTestMgmt
{
    [Binding]
    [CollectionDefinition(nameof(AutomationWebFixtureCollection))]
    public class LoginStepDefinitions
    {
        private readonly LoginScreen _loginScreen;
        private readonly AutomationWebTestFixture _testFixture;

        public LoginStepDefinitions(AutomationWebTestFixture testFixture)
        {
            _testFixture = testFixture;
            _loginScreen = new LoginScreen(testFixture.BrowserHelper);
        }

       [When(@"The user filled the login form with wrong information")]
        public void WhenTheUserFilledTheLoginFormWithWrongInformation()
        {
            //Arrange 
            var user = new User { Username = "wrongUsername", Password = "wrongPassword" };
            _testFixture.User = user;

            //Act 
            _loginScreen.FillLoginForm(user);

            //Assert
            Assert.True(_loginScreen.ValidateFillLoginForm(user));
        }

        [When(@"The user filled the login form with org admin information")]
        public void WhenTheUserFilledTheLoginFormWithOrgAdminInformation()
        {
            //Arrange 
            //Act 
            //Assert
        }

        [Then(@"The invalid credentials message is displayed")]
        public void ThenTheInvalidCredentialsMessageIsDisplayed()
        {
            //Arrange 
            string ErrorMessage = "Your login attempt was not successful. The username or password you entered is incorrect, please try again.";

            //Assert
            Assert.True(_loginScreen.ValidateLoginErrorMessage(ErrorMessage));
        }


        [Then(@"The password required message is displayed")]
        public void ThenThePasswordRequiredMessageIsDisplayed()
        {
            //Arrange 
            string ErrorMessage = "Your login attempt was not successful. A password is required for authentication.";

            //Assert
            Assert.True(_loginScreen.ValidateLoginErrorMessage(ErrorMessage));
        }

        [When(@"The user filled the login form without password")]
        public void WhenTheUserFilledTheLoginFormWithoutPassword()
        {
            //Arrange 
            var user = new User { Username = "sample", Password = "" };
            _testFixture.User = user;

            //Act 
            _loginScreen.FillLoginForm(user);

            //Assert
            Assert.True(_loginScreen.ValidateFillLoginForm(user));
        }

    }
}
