Feature: Create Project Testing
Scenario: Create Project with all fields successfully
    Given the user is logged into the application
    And the user navigates to Create Project page
    When the user fills in all project information
    And the user clicks Create button
    Then all information of the project is shown correctly