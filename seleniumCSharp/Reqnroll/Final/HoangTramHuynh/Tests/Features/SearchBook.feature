Feature: Book Store

  Scenario Outline: Search book with multiple results successfully
    Given the user is on the Book Store page
    When the user inputs book search data
      | BookName      |
      | <SearchText>  |
    Then all books match with input criteria will be displayed

    Examples:
      | SearchText  |
      | Design      |
      | design      |