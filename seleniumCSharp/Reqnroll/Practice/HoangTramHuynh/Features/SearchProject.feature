Feature: Search Project Testing
Scenario: Search project with any criteria successfully
    Given the user is logged into the application
    And the user navigates to Search Project page
    When the user applies search criteria by Name, Location, or Type
    And the user clicks on Search button
    Then all projects matched with the input criteria will be displayed