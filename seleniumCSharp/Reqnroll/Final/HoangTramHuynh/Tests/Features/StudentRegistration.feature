Feature: Student Registration

  Scenario Outline: Register a student information successfully
    Given the user is on Student Registration Form page
    When the user inputs valid data into all fields
    | Field           | Value             |
    | FirstName       | <FirstName>       |
    | LastName        | <LastName>        |
    | Email           | <Email>           |
    | Gender          | <Gender>          |
    | MobileNumber    | <MobileNumber>    |
    | DateOfBirth     | <DateOfBirth>     |
    | Subjects        | <Subjects>        |
    | Hobbies         | <Hobbies>         |
    | PicturePath     | <PicturePath>     |
    | CurrentAddress  | <CurrentAddress>  |
    | State           | <State>           |
    | City            | <City>            |
    And the user clicks on Submit button
    Then the success modal "Thanks for submitting the form" is displayed
    And all submitted student information is displayed correctly

    Examples:
      | FirstName | LastName | Email                  | Gender | MobileNumber     | DateOfBirth  | Subjects                | Hobbies         | PicturePath                | CurrentAddress   | State | City  |
      | Hoang     | Tram     | hoangtram04@gmail.com  | Female | 0987654321       | 15 May 2002  | Maths, Computer Science | Sports, Reading | Tests/TestData/avatar.png  | Ho Chi Minh City | NCR   | Delhi |