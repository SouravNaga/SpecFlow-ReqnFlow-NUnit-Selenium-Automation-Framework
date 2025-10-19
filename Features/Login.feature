Feature: Login Functionality check

@validLogin
Scenario: Login functionality with valid creds
	Given Open the url
	When User enter username
	And User enter password
	And User click submit button
	Then Login should be successful
