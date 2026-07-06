Feature:Login Testing

  Scenario: Login with valid account successfully
    Given the user visits the TMS website
    When the user logs in with username "Admin2" and password "Fxu1tw^E"
    And the user clicks on Login button
    Then the user is logged into the system successfully

  Scenario Outline: Login with invalid account
    Given the user visits the TMS website
    When the user logs in with username "<username>" and password "<password>"
    And the user clicks on Login button
    Then the error message will be displayed

    Examples:
      | username | password |
      | Admin2   |          |
      |          | qp$#tGu^ |
      | Admin1   | qp$#tGu3 |