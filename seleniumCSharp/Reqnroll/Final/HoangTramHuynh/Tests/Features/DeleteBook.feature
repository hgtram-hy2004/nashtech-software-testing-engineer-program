Feature: Delete Book

  Scenario Outline: Delete book successfully
    Given the book is available in the user's collection
      | Username   | Password   | BookName   |
      | <Username> | <Password> | <BookName> |
    And the user logs into the application
    And the user is on the Profile page
    When the user searches for the book
    And the user clicks on Delete icon of the book
    And the user clicks on OK button
    And the user clicks on OK button of alert "Book deleted."
    Then the book is not shown

    Examples:
      | BookName         | Username      | Password      |
      | Git Pocket Guide | hgtram.hy04   | Hgtram@hy2004 |