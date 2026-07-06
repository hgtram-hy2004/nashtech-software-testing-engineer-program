using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using HuynhHoangTram.Page;
using HuynhHoangTram.Utils;
using HuynhHoangTram.DataObject;
using HuynhHoangTram.Core;
using HuynhHoangTram.Component;
using OpenQA.Selenium;
namespace HuynhHoangTram.Test
{
    [TestFixture]
    public class RegisterTest : BaseTest
    {
        private RegisterPage register = null!;
        HomePage home = new HomePage();
        [SetUp]
        public void PageSetUp()
        {
            register = new RegisterPage();
            home.NavigateToForms();
            register.NavigateToPracticeForm();

            Assert.That(
                register.IsRegistrationFormDisplayed(),
                Is.True,
                "Registration form title is not displayed after navigating to Practice Form."
            );
        }
        [Test]
        public void RegisterStudentWithAllFieldsSuccessfully()
        {
            RegisterData registerData = JsonUtils.ReadJsonFile<RegisterData>("TestData\\RegisterData.json");

            Assert.That(
                register.IsRegistrationFormDisplayed(),
                Is.True,
                "Registration form title is not displayed after navigating to Practice Form."
            );

            register.RegisterStudent(
                registerData.FirstName,
                registerData.LastName,
                registerData.Gender,
                registerData.MobileNumber,
                registerData.Email,
                registerData.DateOfBirth,
                registerData.Subjects,
                registerData.Hobbies,
                registerData.PicturePath,
                registerData.CurrentAddress,
                registerData.State,
                registerData.City
            );

            Assert.That(
                register.IsSubmittedModalDisplayed(),
                Is.True,
                "Submitted modal is not displayed."
            );

            Assert.Multiple(() =>
            {
                Assert.That(register.GetSubmittedValue("Student Name"),Is.EqualTo(registerData.FirstName + " " + registerData.LastName).IgnoreCase,
                    "Student Name is not correct.");

                Assert.That(register.GetSubmittedValue("Student Email"),Is.EqualTo(registerData.Email).IgnoreCase,
                    "Student Email is not correct.");

                Assert.That(register.GetSubmittedValue("Gender"),Is.EqualTo(registerData.Gender).IgnoreCase,
                    "Gender is not correct.");

                Assert.That(register.GetSubmittedValue("Mobile"),Is.EqualTo(registerData.MobileNumber),
                    "Mobile is not correct.");

                Assert.That(DatePicker.IsDateMatched(register.GetSubmittedValue("Date of Birth"),registerData.DateOfBirth),
                    Is.True,
                    "Date of Birth is not correct.");

                Assert.That(WebObjectExtension.IsTextEqual(register.GetSubmittedValue("Subjects"),string.Join(", ", registerData.Subjects)),
                    Is.True,
                    "Subjects are not correct.");

                Assert.That(WebObjectExtension.IsOptionsMatched(register.GetSubmittedValue("Hobbies"),registerData.Hobbies),
                    Is.True,
                    "Hobbies are not correct."
                );

                Assert.That(register.GetSubmittedValue("Picture"),Is.EqualTo(Path.GetFileName(registerData.PicturePath)).IgnoreCase,
                    "Picture is not correct.");

                Assert.That(register.GetSubmittedValue("Address"),Is.EqualTo(registerData.CurrentAddress).IgnoreCase,
                    "Address is not correct.");

                Assert.That(register.GetSubmittedValue("State and City"),Is.EqualTo(registerData.State + " " + registerData.City).IgnoreCase,
                    "State and City is not correct.");
            });
        }
        [Test]
        public void RegisterStudentWithRequiredFieldsSuccessfully()
        {
           RegisterData registerData = JsonUtils.ReadJsonFile<RegisterData>("TestData\\RegisterData.json");

            register.RegisterStudent(
                registerData.FirstName,
                registerData.LastName,
                registerData.Gender,
                registerData.MobileNumber
            );

            Assert.That(
                register.IsSubmittedModalDisplayed(),
                Is.True,
                "Submitted modal is not displayed."
            );

             Assert.Multiple(() =>
            {
                Assert.That(register.GetSubmittedValue("Student Name"),Is.EqualTo(registerData.FirstName + " " + registerData.LastName).IgnoreCase,
                    "Student Name is not correct.");

                Assert.That(register.GetSubmittedValue("Gender"),Is.EqualTo(registerData.Gender).IgnoreCase,
                    "Gender is not correct.");

                Assert.That(register.GetSubmittedValue("Mobile"),Is.EqualTo(registerData.MobileNumber),
                    "Mobile is not correct.");
            });
        }
    }
}