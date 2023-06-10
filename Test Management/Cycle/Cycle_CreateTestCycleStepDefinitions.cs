using OpenQA.Selenium.DevTools.V111.Network;
using SeleniumExtras.WaitHelpers;
using System;
using System.Net.NetworkInformation;
using TechTalk.SpecFlow;
using TestJamaTestMgmt.Config;
using TestJamaTestMgmt.Test_Management.Cycle;
using TestJamaTestMgmt.Test_Management.Login;

namespace TestJamaTestMgmt
{
    [Binding]
    [CollectionDefinition(nameof(AutomationWebFixtureCollection))]
    public class Cycle_CreateTestCycleStepDefinitions
    {
        private readonly AutomationWebTestFixture _testsFixture;
        private readonly CycleScreen _cycleScreen;
        private readonly LoginScreen _loginScreen;
        private readonly CycleDetail _cycleDetail;
        //private string _urlPlan;

        public Cycle_CreateTestCycleStepDefinitions(AutomationWebTestFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cycleScreen = new CycleScreen(testsFixture.BrowserHelper);
            _loginScreen = new LoginScreen(testsFixture.BrowserHelper);
            _cycleDetail = new CycleDetail{ name        = "CycleNameTestx",
                                            description = "CycleDescriptionTest"
                                          };
        }

        [Given(@"The user is logged")]
        public void GivenTheUserIsLogged()
        {
            //Arrange
            var user = new User();
            user.Username = "sample";
            user.Password = "password";

            _testsFixture.User = user;

            //Act
            bool login = _loginScreen.Login(user);

            //Assert
            
            Assert.True(login);
        }

        [Given(@"Goes to Projects tab")]
        public void GivenGoesToProjectsTab()
        {
            _cycleScreen.GoToProjects();

            if (!_cycleScreen.ValidatePage("Projects")) return;
        }


        [Given(@"There is at least one test case")]
        public void GivenThereIsAtLeastOneTestCase()
        {
            //Arrange
            string cssIdTestCaseSet = "extdd-28";                   //css id of the DIV regards to the SET o test case to test
            string cssClassTestCaseList = "x-tree-node";
            string xpathTestCaseList = "//*[contains(@id, 'extdd-28')]/following-sibling::ul";
            
            //Act
            _cycleScreen.ExpandExplorerSet(cssIdTestCaseSet);
            bool thereIsTestCase = _cycleScreen.DoesTestCasesExitsInTestSet(xpathTestCaseList, cssClassTestCaseList);

            //Assert
            Assert.True(thereIsTestCase);
        }

        [Given(@"Goes to Test Plan")]
        public void GivenGoesToTestPlan()
        {
            //Arrange
            string cssIdTestPlan = "ext-comp-1190";
            string testPlanCss   = "j-selector-item";
            int testPlanPosition = 2; //Test case position (array starts in Zero -> POSITION 1 IS ELEMENT 2 )

            //Act
            _cycleScreen.OpenTestPlan(cssIdTestPlan, testPlanCss, testPlanPosition);

            //Assert
            Assert.True(_cycleScreen.ValidatePage("testPlans"));
        }

        [Given(@"Goes to Test Plan without Test Runs")]
        public void GivenGoesToTestPlanWithoutTestRuns()
        {
            //Arrange
            string cssIdTestPlan = "ext-comp-1190";
            string testPlanCss = "j-selector-item";
            int testPlanPosition = 3; //Test case position (array starts in Zero -> POSITION 1 IS ELEMENT 2 )

            //Act
            _cycleScreen.OpenTestPlan(cssIdTestPlan, testPlanCss, testPlanPosition);

            //Assert
            Assert.True(_cycleScreen.ValidatePage("testPlans"));
        }


        [Given(@"Test Plan has Test Cases")]
        public void GivenTestPlanHasTestCases()
        {
            //Arrange
            //List of Groups
            string xpathTestCaseList = "//*[contains(@class, 'x-grid3-body')]/div";

            _cycleScreen.OpenTestCasesFromTestPlan();

            if (!_cycleScreen.isTestCasesFromTestPlanVisible()) return;

            //Act
            bool isNotEmpty = _cycleScreen.DoesTestCasesExitsInTestPlan(xpathTestCaseList);

            //Assert
            Assert.True(isNotEmpty);
        }

        [Given(@"Test Runs page is openned")]
        public void GivenTestRunsPageIsOpenned()
        {
            //Act
            _cycleScreen.OpenTestRunsList();

            //Assert
            Assert.True(_cycleScreen.isTestRunsListVisible());
        }

        [When(@"The user click on create a new test cycle")]
        public void WhenTheUserClickOnCreateANewTestCycle()
        {
            //Act 
            _cycleScreen.AddCycle();

            //Assert
            Assert.True(_cycleScreen.IsAddCycleModalOpened());
        }

        [Then(@"Add All details to Cycle")]
        public void ThenAddAllDetailsToCycle()
        {
            //Arrange 
            
            //Act 
            _cycleScreen.FillCycleDetail(_cycleDetail);

        }

        [Then(@"Test Runs list is displayed")]
        public void ThenTestRunsListIsDisplayed()
        {
            //Assert
            Assert.True(_cycleScreen.CycleExists(_cycleDetail.name));
        }

        [Given(@"There is ten or more test cases")]
        public void GivenThereIsTenOrMoreTestCases()
        {
            //Arrange
            string cssIdTestCaseSet = "extdd-28";                   //css id of the DIV regards to the SET o test case to test
            string cssClassTestCaseList = "x-tree-node";
            string xpathTestCaseList = "//*[contains(@id, 'extdd-28')]/following-sibling::ul";

            //Act
            _cycleScreen.ExpandExplorerSet(cssIdTestCaseSet);
            bool thereIsTenTestCase = _cycleScreen.DoesTenOrMoreTestCasesExitsInTestSet(xpathTestCaseList, cssClassTestCaseList);

            //Assert
            Assert.True(thereIsTenTestCase);
        }

        [Given(@"Test Plan has at least ten Test Cases")]
        public void GivenTestPlanHasAtLeastTenTestCases()
        {
            //Arrange
            //List of Test Cases
            string xpathTestCaseList = "//*[contains(@class, 'x-grid3-body')]/div";

            _cycleScreen.OpenTestCasesFromTestPlan();

            if (!_cycleScreen.isTestCasesFromTestPlanVisible()) return;

            //Act
            bool isNotEmpty = _cycleScreen.DoesTenOrMoreTestCasesExitsInTestPlan(xpathTestCaseList);

            //Assert
            Assert.True(isNotEmpty);
        }

        [Given(@"There is two groups")]
        public void GivenThereIsTwoGroups()
        {
            //Arrange 
            //List of Groups
            string xpathTestGroupList = "//*[contains(@class, 'x-grid3-body')]";

            _cycleScreen.OpenTestCasesFromTestPlan();

            if (!_cycleScreen.isTestCasesFromTestPlanVisible()) return;

            //Act
            bool isNotEmpty = _cycleScreen.DoesTwoOrMoreTestGroupsExitsInTestPlan(xpathTestGroupList);

            //Assert
            Assert.True(isNotEmpty);
        }

        [Given(@"Test Plan has no test cases")]
        public void GivenTestPlanHasNoTestCases()
        {
            //Arrange
            //List of Test Cases
            string xpathTestCaseList = "//*[contains(@class, 'x-grid3-body')]/div";

            _cycleScreen.OpenTestCasesFromTestPlan();

            if (!_cycleScreen.isTestCasesFromTestPlanVisible()) return;

            //Act
            bool isNotEmpty = _cycleScreen.DoesTestCasesExitsInTestPlan(xpathTestCaseList);

            //Assert
            Assert.False(isNotEmpty);
        }

        [Then(@"Save button is disabled")]
        public void ThenSaveButtonIsDisabled()
        {
            //Arrange 
            //Act 
            //Assert
        }


        [Then(@"Add no details to Cycle")]
        public void ThenAddNoDetailsToCycle()
        {
            //Arrange 

            _cycleDetail.name = "";
            _cycleDetail.description = "";

            //Act 
            _cycleScreen.FillCycleDetail(_cycleDetail);
        }

        [Then(@"An Error Toast is displayed")]
        public void ThenAnErrorToastIsDisplayed()
        {
            //Arrange 
            string toastErrorXpath = "//*[contains(@class, 'toast--type-error')]//p";

            //Act 
            var ToastMessage = _cycleScreen.GetToastError(toastErrorXpath);

            //Assert
            //Message Header
            Assert.Contains(expectedSubstring: "Unexpected error", ToastMessage[0]);
            //Message Body
            Assert.Contains(expectedSubstring: "Try adding test cases again. If the issue persists, contact your administrator.", ToastMessage[1]);
        }

        [Given(@"Test Cycle Exists")]
        public void GivenTestCycleExists()
        {
            Assert.True(_cycleScreen.CycleExists(_cycleDetail.name));
        }

        [When(@"The user click on Delete Cycle")]
        public void WhenTheUserClickOnDeleteCycle()
        {
            //Act 
            _cycleScreen.DeleteCycle();

            //Assert
            Assert.True(_cycleScreen.IsDeleteCycleConfirmationOpened());
        }

        [When(@"Confirm Cycle Deletion")]
        public void WhenConfirmCycleDeletion()
        {
            //Arrange
            //string yesButtonXpath = "//button[contains(text(), 'Yes')]";
            //
            //_cycleScreen.ActionModalConfirmation(yesButtonXpath);
        }
    }

}
