Feature: Cycle - Create Test Cycle
	As a user
	I wish to create a new cycle
	So i can add test runs
	and Run the Test Runs as needed

@tag1
Scenario: Create a new test cycle
	Given The user is logged
		And Goes to Projects tab
		And There is at least one test case
		And Goes to Test Plan
		And Test Plan has Test Cases
		And Test Runs page is openned
	When The user click on create a new test cycle
	Then Add All details to Cycle
		And Test Runs list is displayed

Scenario: Create a new test cycle with 10 test runs
	Given The user is logged
		And Goes to Projects tab
		And There is ten or more test cases
		And Goes to Test Plan
		And Test Plan has at least ten Test Cases
		And Test Runs page is openned
	When The user click on create a new test cycle
	Then Add All details to Cycle
		And Test Runs list is displayed

Scenario: Create a new test cycle with 2 Groups 
	Given The user is logged
		And Goes to Projects tab
		And Goes to Test Plan
		And There is two groups
		And Test Runs page is openned
	When The user click on create a new test cycle
	Then Add All details to Cycle
		And Test Runs list is displayed

Scenario: Create a new test cycle without Test Runs
	Given The user is logged
		And Goes to Projects tab
		And Goes to Test Plan without Test Runs
		And Test Plan has no test cases
		And Test Runs page is openned
	When The user click on create a new test cycle
	Then Add All details to Cycle
		And Test Runs list is displayed
		And An Error Toast is displayed

Scenario: Create a new test cycle without Details
	Given The user is logged
		And Goes to Projects tab
		And Goes to Test Plan
		And Test Plan has Test Cases
		And Test Runs page is openned
	When The user click on create a new test cycle
	Then Add no details to Cycle
		And Save button is disabled

Scenario: Delete test cycle
	Given The user is logged
		And Goes to Projects tab
		And Goes to Test Plan
		And Test Runs page is openned
		And Test Cycle Exists
	When The user click on Delete Cycle
	Then Test Runs list is displayed