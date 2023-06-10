using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestJamaTestMgmt.Config;

namespace TestJamaTestMgmt.Test_Management.Cycle
{
    public class CycleScreen : PageObjectModel
    {
        public CycleScreen(SeleniumHelper helper) : base(helper) { }

        public void UserLogin(){}

        public void GoToProjects()
        {
            Helper.GoToUrl(Helper.Configuration.ProjectsUrl);
        }

        public bool ValidatePage(string page)
        {
            return Helper.ValidateUrlContent(page);
        }

        public void OpenTestPlanExplorer()
        {
            Helper.GetElementById("j-perspective-nav-panel-center__ext-comp-1188")
                    .FindElement(By.ClassName("x-tab-right"))
                    .Click();
        }

        public void ExpandExplorerSet(string cssIdofSet)
        {
            Helper.GetElementById(cssIdofSet)
                    .FindElement(By.ClassName("x-tree-elbow-plus"))     //ccs class of the "+" button to expand the SET
                    .Click();
        }

        public bool isTestPlanExplorerVisible()
        {
            return Helper.ValidateElementById("ext-comp-1188");
        }

        public bool IsthereTestPlans()
        {
            var xpath = $"//div[contains(@id,'ext-comp-1190')]//div[contains(@class, 'j-selector-item')]";

            return Helper.GetListByXpath(xpath).Any();
        }

        public void OpenTestPlanDetail(string cssIdTestPlan, string testCaseCss, int position = 0)
        {
            Helper.GetElementById(cssIdTestPlan)                // Get Test plan
                .FindElements(By.ClassName(testCaseCss))  // Get All test plans created
                .ElementAt(position)                            // Select the specific test plan
                .Click();                                       // Open the Test Case Single Item View (SIV)
        }

        public bool isTestPlanDetailVisible()
        {
            return Helper.GetUrl().Contains("testPlans");
        }

        public void OpenTestCasesFromTestPlan()
        {
            Helper.GetElementByClass("testCases-view").Click();
        }

        public bool isTestCasesFromTestPlanVisible()
        {
            return Helper.GetUrl().Contains("testCases");
        }

        public void OpenTestRunsList()
        {
            Helper.GetElementByClass("testRuns-view").Click();
        }

        public bool isTestRunsListVisible()
        {
            return Helper.GetUrl().Contains("testRuns");
        }

        public int HowManyTestPlansExits()
        {
            return Helper.GetElementById("ext-comp-1190")
                .FindElements(By.ClassName("j-selector-item"))
                .Count();
        }

        public bool DoesTestCasesExitsInTestPlan(string xpathTestCaseList)
        {
            bool existTestCase;

            try
            {
                existTestCase = Helper.GetListByXpath(xpathTestCaseList).Any();
            }
            catch
            {
                return false;
            }
            
            return existTestCase;
            
        }

        public void OpenTestPlan(string cssIdTestPlanSet, string testCaseCss, int testPlanPosition = 0)
        {
            //Arrange 
            OpenTestPlanExplorer();

            if (!isTestPlanExplorerVisible()) return;

            //Act 
            OpenTestPlanDetail(cssIdTestPlanSet, testCaseCss, testPlanPosition);

            if (!isTestPlanDetailVisible()) return;

        }
        public void ClickOnCycle()
        {
            Helper.ClickByXPath("//*[contains(@class, 'j-current-cycle-label')]//button");
        }

        public void AddCycle()
        {
            ClickOnCycle();

            //Click on ADD CYCLE button
            Helper.ClickLinkByText("Add cycle");
        }

        public void AddFirstCycle()
        {
            //Click on ADD FIRST TEST CYCLE LINK
            Helper.GetElementById("j-perspective-center-view-panel-item-panel-testplan-testRuns-j-create-first-obj").Click();
        }

        public void DeleteCycle()
        {
            ClickOnCycle();

            //Click on ADD CYCLE button
            Helper.ClickLinkByText("Delete cycle");
        }

        public bool IsAddCycleModalOpened()
        {
            //Click on ADD CYCLE button
            return Helper.GetElementByClass("test-cycle-modal").Displayed;
        }

        public bool IsDeleteCycleConfirmationOpened()
        {
            string confirmationMessageXpath = "//*[contains(@class, 'ext-mb-question')]/following-sibling::div/span";

            //Click on ADD CYCLE button
            return Helper.GetElementByXpath(confirmationMessageXpath).Displayed;
        }

        public void ActionModalConfirmation(string ButtonXpath)
        {
            IWebElement button = Helper.GetElementByXpath(ButtonXpath);
            
            button.Click();

            Helper.waitForElementToVanish(button, 10);
        }

        public void FillCycleDetail(CycleDetail detail)
        {
            string xpathModal = $"//div[contains(@class, 'dialog dialog__content--after-open dialog--type-modal test-cycle-modal dialog dialog--type-modal')]";
            IWebElement modal = Helper.GetElementByXpath(xpathModal);

            //clean date fields
            Helper.GetElementById("startDate").Clear();
            Helper.GetElementById("endDate").Clear();

            Helper.FillTextBoxById("name", detail.name);
            Helper.FillTextBoxById("startDate", detail.startDate.ToString());
            Helper.FillTextBoxById("endDate", detail.endDate.ToString());

            //Fill Description Field
            string descriptionXpath = $"//*[contains(text(), 'Description:')]/following-sibling::div/div[2]/div/div/div/p";
            Helper.GetElementByXpath(descriptionXpath).SendKeys(detail.description);
            Helper.GetElementByXpath(descriptionXpath).Click();

            //Click on NEXT button
            Helper.ClickByXPath($"//*[contains(@class, 'wizard-footer')]/div[2]/button");

            //Click on SAVE button
            Helper.ClickByXPath($"//*[contains(@class, 'wizard-footer')]/div[3]/button");

            //Wait Add Cycle modal to Vanish/Disappear
            Helper.waitForElementToVanish(modal, 5);
        }

        public bool CycleExists(string cycleName)
        {
            //Arrange
            
            //Xpath of the selected CYCLE
            string xpathCycleName = $"//div[contains(@class, 'x-combo-list-item x-combo-selected')]";
            
            //Xpath of the dropdown button
            string xpathDropDownButton = $"//*[contains(@id, 'j-perspective-center-view-panel-item-panel-testplan-testRuns-current-cycle-combo')]/following-sibling::img";

            //Open Cycles DropDown ( So the list is displayed and elements are added to the DOM )
            Helper.GetElementClickable(xpathDropDownButton).Click();

            //Act
            bool cycleCreated = Helper.GetElementByClass("x-combo-selected").Text.Contains(cycleName);

            //Close Cycles DropDown
            Helper.GetElementByXpath(xpathDropDownButton).Click();

            //Confirm if Cycle was created by searching the Cycle Name on the Cycles DropDown
            return cycleCreated;
        }

        public bool DoesTestCasesExitsInTestSet(string xpathTestCaseList, string cssClassTestCaseList)
        {

            bool thereIsTestCase = Helper.GetElementByXpath(xpathTestCaseList)
                                    .FindElements(By.ClassName(cssClassTestCaseList))
                                    .Any();

            return thereIsTestCase ? thereIsTestCase : false;

        }

        public void ClickOnExpandSetOnExplorer(string xpath)
        { 
            Helper.GetElementByXpath(xpath).Click();
        }

        public bool DoesTenOrMoreTestCasesExitsInTestSet(string xpathTestCaseList, string cssClassTestCaseList)
        {
                //Get how much test cases exists
                int testCasesCount = Helper.GetElementByXpath(xpathTestCaseList)
                                            .FindElements(By.ClassName(cssClassTestCaseList))
                                            .Count();

                if (testCasesCount < 10) return false;

                return true;
        }

        public bool DoesTenOrMoreTestCasesExitsInTestPlan(string xpathTestCaseList)
        {
            if (Helper.GetListByXpath(xpathTestCaseList).Count() < 10) return false;

            return true;
        }
        public bool DoesTwoOrMoreTestGroupsExitsInTestPlan(string xpathTestGroupList)
        {
            if (Helper.GetListByXpath(xpathTestGroupList).Count() < 2) return false;

            return true;
           
        }
        public string[] GetToastError(string toastErrorXpath)
        {
            var pElement = Helper.GetListByXpath(toastErrorXpath);
            string[] toastMessage = new string[] { pElement.ElementAt(0).Text, pElement.ElementAt(1).Text };
            
            return toastMessage;
        }
    }
}
