using HoangTramHuynh.Component;
using HoangTramHuynh.Contexts;
using HoangTramHuynh.DataObject;
using HoangTramHuynh.Page;
using HoangTramHuynh.Core.UI;
using NUnit.Framework;
using Reqnroll;
using Reqnroll.Assist;

namespace HoangTramHuynh.StepDefinitions
{
    [Binding]
    public class StudentRegistrationStep
    {
        private readonly StudentRegistrationContext _studentRegistrationContext;
        private readonly HomePage _homePage = new HomePage();
        private readonly RegisterPage _registerPage = new RegisterPage();

        public StudentRegistrationStep(StudentRegistrationContext studentRegistrationContext)
        {
            _studentRegistrationContext = studentRegistrationContext;
        }

        [Given("the user is on Student Registration Form page")]
        public void GivenTheUserIsOnStudentRegistrationFormPage()
        {
            _homePage.NavigateToForms();
            _registerPage.NavigateToPracticeForm();

            Assert.That(
                _registerPage.IsRegistrationFormDisplayed(),
                Is.True,
                "Registration form is not displayed."
            );
        }

        [When("the user inputs valid data into all fields")]
        public void WhenTheUserInputsValidDataIntoAllFields(Reqnroll.Table table)
        {
            _studentRegistrationContext.RegisterData = table.CreateInstance<RegisterData>();

            RegisterData registerData = _studentRegistrationContext.RegisterData;

            string[] subjects = SplitOptions(registerData.Subjects);
            string[] hobbies = SplitOptions(registerData.Hobbies);

            _registerPage.FillRegistrationForm(
                registerData.FirstName,
                registerData.LastName,
                registerData.Gender,
                registerData.MobileNumber,
                registerData.Email,
                registerData.DateOfBirth,
                subjects,
                hobbies,
                registerData.PicturePath,
                registerData.CurrentAddress,
                registerData.State,
                registerData.City
            );
        }

        [When("the user clicks on Submit button")]
        public void WhenTheUserClicksOnSubmitButton()
        {
            _registerPage.ClickSubmitButton();
        }

        [Then("the success modal {string} is displayed")]
        public void ThenTheSuccessModalIsDisplayed(string expectedMessage)
        {
            Assert.That(
                _registerPage.IsSubmittedModalDisplayed(),
                Is.True,
                $"Success modal '{expectedMessage}' is not displayed."
            );
        }

        [Then("all submitted student information is displayed correctly")]
        public void ThenAllSubmittedStudentInformationIsDisplayedCorrectly()
        {
            RegisterData registerData = _studentRegistrationContext.RegisterData;

            string[] expectedSubjects = SplitOptions(registerData.Subjects);
            string[] expectedHobbies = SplitOptions(registerData.Hobbies);

            Assert.Multiple(() =>
            {
                Assert.That(
                    _registerPage.GetSubmittedValue("Student Name"),
                    Is.EqualTo(registerData.FirstName + " " + registerData.LastName).IgnoreCase,
                    "Student Name is not correct."
                );

                Assert.That(
                    _registerPage.GetSubmittedValue("Student Email"),
                    Is.EqualTo(registerData.Email).IgnoreCase,
                    "Student Email is not correct."
                );

                Assert.That(
                    _registerPage.GetSubmittedValue("Gender"),
                    Is.EqualTo(registerData.Gender).IgnoreCase,
                    "Gender is not correct."
                );

                Assert.That(
                    _registerPage.GetSubmittedValue("Mobile"),
                    Is.EqualTo(registerData.MobileNumber),
                    "Mobile is not correct."
                );

                Assert.That(
                    DatePicker.IsDateMatched(
                        _registerPage.GetSubmittedValue("Date of Birth"),
                        registerData.DateOfBirth
                    ),
                    Is.True,
                    "Date of Birth is not correct."
                );

                Assert.That(
                    WebObjectExtension.IsTextEqual(
                        _registerPage.GetSubmittedValue("Subjects"),
                        string.Join(", ", expectedSubjects)
                    ),
                    Is.True,
                    "Subjects are not correct."
                );

                Assert.That(
                    WebObjectExtension.IsOptionsMatched(
                        _registerPage.GetSubmittedValue("Hobbies"),
                        expectedHobbies
                    ),
                    Is.True,
                    "Hobbies are not correct."
                );

                Assert.That(
                    _registerPage.GetSubmittedValue("Picture"),
                    Is.EqualTo(Path.GetFileName(registerData.PicturePath)).IgnoreCase,
                    "Picture is not correct."
                );

                Assert.That(
                    _registerPage.GetSubmittedValue("Address"),
                    Is.EqualTo(registerData.CurrentAddress).IgnoreCase,
                    "Address is not correct."
                );

                Assert.That(
                    _registerPage.GetSubmittedValue("State and City"),
                    Is.EqualTo(registerData.State + " " + registerData.City).IgnoreCase,
                    "State and City is not correct."
                );
            });
        }

        private string[] SplitOptions(string? value)
        {
            return (value ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(option => option.Trim())
                .ToArray();
        }
    }
}