Feature: Login Functionality check using dataprovider
@dataProvider
Scenario Outline: Login functionality check
  Given Open the url
  When Username enters <username>
  And Password enters <password>
  And User click submit button
  Then Login should be successful

Examples:
  | username | password     |
  | student  | Password123  |
  | admin    | Admin@456    |