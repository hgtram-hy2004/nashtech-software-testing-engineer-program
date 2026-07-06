using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using HuynhHoangTram.Component;
using OpenQA.Selenium;

namespace HuynhHoangTram.Page
{
    public class RegisterPage
    {
        private readonly Menu _sideBarMenu;
        public RegisterPage()
        {
            _sideBarMenu = new Menu(SideBarMenu, SideBarMenuItem);
        }
        private WebObject SideBarMenu(string menuText)
        {
            return new WebObject(By.XPath($"//div[contains(@class,'left-pannel')]//div[contains(@class,'header-text') and normalize-space(.)='{menuText}']/ancestor::div[contains(@class,'header-wrapper')]"), menuText + " Menu");
        }

        private WebObject SideBarMenuItem(string menuText)
        {
            return new WebObject(By.XPath($"//div[contains(@class,'left-pannel')]//span[@class='text' and normalize-space(.)='{menuText}']/ancestor::a"), menuText + " Item Menu");
        }
        private readonly WebObject _formTitle = new WebObject(By.XPath("//h5[normalize-space(.)='Student Registration Form']"), "Student Registration Form Title");
        private readonly WebObject _firstnameinput = new WebObject(By.Id("firstName"), "First Name Input");
        private readonly WebObject _lastnameinput = new WebObject(By.Id("lastName"), "Last Name Input");
        private readonly WebObject _emailinput = new WebObject(By.Id("userEmail"), "Email Input");
        private WebObject GenderRadio(string value)
        {
            return new WebObject(By.XPath($"//div[@id='genterWrapper']//input [@value='{value}']"), "Gender Radio " + value + "Input");
        }
        private readonly WebObject _mobileinput = new WebObject(By.Id("userNumber"), "Mobile Input");
        private readonly WebObject _dateofbirthinput = new WebObject(By.Id("dateOfBirthInput"), "Date Of Birth Input");
        private readonly WebObject _calendarContainer = new WebObject(By.CssSelector("div.react-datepicker"), "Date Picker Calendar");
        private readonly WebObject _monthdropdown = new WebObject(By.CssSelector("select.react-datepicker__month-select"), "Month Dropdown");
        private readonly WebObject _yeardropdown = new WebObject(By.CssSelector("select.react-datepicker__year-select"), "Year Dropdown");
        private WebObject MonthOption(string monthValue)
        {
            return new WebObject(By.XPath($"//select[contains(@class,'react-datepicker__month-select')]/option[@value='{monthValue}']"), "Month Option " + monthValue);
        }

        private WebObject YearOption(string year)
        {
            return new WebObject(By.XPath($"//select[contains(@class,'react-datepicker__year-select')]/option[@value='{year}']"), "Year Option " + year);
        }

        private WebObject DayOption(string day)
        {
            return new WebObject(By.XPath($"//div[contains(@class,'react-datepicker')]//div[contains(@class,'react-datepicker__day') and not(contains(@class,'outside-month')) and normalize-space(.)='{day}']"), "Day " + day);
        }
        private readonly WebObject _nextbutton = new WebObject(By.CssSelector("button.react-datepicker__navigation--next"), "Next Button");
        private readonly WebObject _previousButton = new WebObject(By.CssSelector("button.react-datepicker__navigation--previous"), "Previous Button");
        private readonly WebObject _subjectsInput = new WebObject(By.CssSelector("input#subjectsInput"), "Subjects Input");
        private readonly WebObject _subjectsDropdownList = new WebObject(By.XPath("//div[contains(@class,'subjects-auto-complete__menu')]"), "Subjects Dropdown List");

        private WebObject SubjectOption(string subject)
        {
            return new WebObject(By.XPath($"//div[contains(@class,'subjects-auto-complete__option') and normalize-space(.)='{subject}']"), subject + " Subject Option");
        }
        private WebObject HobbiesCheckbox(string value)
        {
            return new WebObject(By.XPath($"//div[@id='hobbiesWrapper']//label[normalize-space()='{value}']"), "Hobbies Checkbox " + value + "Input");
        }
        private readonly FileUpload _pictureupload = new FileUpload(new WebObject(By.CssSelector("input#uploadPicture"), "Picture Upload"));
        private readonly WebObject _currentaddressinput = new WebObject(By.Id("currentAddress"), "Current Address Input");
        private readonly WebObject _stateDropdown = new WebObject(By.CssSelector("div#state"), "State Dropdown");
        private readonly WebObject _cityDropdown = new WebObject(By.CssSelector("div#city"), "City Dropdown");
        private readonly WebObject _DropdownList = new WebObject(By.XPath("//div[@role='listbox']"), "State and City Dropdown List");
        private WebObject DropdownOption(string optionText)
        {
            return new WebObject(By.XPath($"//div[@role='option' and normalize-space(.)='{optionText}']"), optionText + " Dropdown Option");
        }
        private readonly WebObject _submitbutton = new WebObject(By.CssSelector("button#submit"), "Submit Button");
        private readonly WebObject _submittedmodal = new WebObject(By.CssSelector("div.modal-content"), "Submitted Modal");
        private readonly WebObject _submittedmodalTitle = new WebObject(By.XPath("//div[@class='modal-title h4' and normalize-space(.)='Thanks for submitting the form']"), "Submitted Modal Title");
        public void NavigateToPracticeForm()
        {
            _sideBarMenu.SelectMenuItem("Practice Form");
        }
        public bool IsRegistrationFormDisplayed()
        {
            return _formTitle.IsElementDisplayed();
        }
        private WebObject SubmittedValueByLabel(string label)
        {
            return new WebObject(By.XPath($"//div[contains(@class,'modal-content')]//td[normalize-space(.)='{label}']/following-sibling::td"), label + " Submitted Value");
        }

        public void EnterFirstName(string firstName)
        {
            _firstnameinput.EnterText(firstName);
        }

        public void EnterLastName(string lastName)
        {
            _lastnameinput.EnterText(lastName);
        }

        public void EnterEmail(string email)
        {
            _emailinput.EnterText(email);
        }

        public void SelectGender(string gender)
        {
            GenderRadio(gender).ClickOnElement();
        }

        public void EnterMobileNumber(string mobileNumber)
        {
            _mobileinput.EnterText(mobileNumber);
        }

        public void SelectDateOfBirth(string dateOfBirth)
        {
            DatePicker datePicker = new DatePicker(
                _dateofbirthinput,
                _calendarContainer,
                _yeardropdown,
                _monthdropdown,
                _nextbutton,
                _previousButton,
                YearOption,
                MonthOption,
                DayOption
            );

            datePicker.SelectDate(dateOfBirth);
        }

        private AutoCompleteInput SubjectsAutoComplete()
        {
            return new AutoCompleteInput(
                _subjectsInput,
                _subjectsDropdownList,
                SubjectOption
            );
        }

        public void SelectSubject(string text, string subject)
        {
            SubjectsAutoComplete().TypeTextAndSelectOption(text, subject);
        }

        public void SelectSubjects(string[]? subjects)
        {
            if (subjects == null || subjects.Length == 0)
            {
                return;
            }

            foreach (string subject in subjects)
            {
                SelectSubject(subject.Substring(0, 1), subject);
            }
        }
        public void SelectHobby(string hobby)
        {
            HobbiesCheckbox(hobby).ClickOnElement();
        }
        public void SelectHobbies(params string[] hobbies)
        {
            foreach (string hobby in hobbies)
            {
                SelectHobby(hobby);
            }
        }
        public void SelectPicture(string filePath)
        {
            _pictureupload.SelectFile(filePath);
        }

        public void EnterCurrentAddress(string address)
        {
            _currentaddressinput.EnterText(address);
        }

        public void SelectState(string state)
        {
            Dropdown dropdown = new Dropdown(
            _stateDropdown,
            _DropdownList,
            DropdownOption);
            dropdown.SelectOption(state);
        }

        public void SelectCity(string city)
        {
            Dropdown dropdown = new Dropdown(
            _cityDropdown,
            _DropdownList,
            DropdownOption);
            dropdown.SelectOption(city);
        }

        public void ClickSubmitButton()
        {
            _submitbutton.ScrollToElement();
            _submitbutton.ClickOnElement();
        }

        public void FillRegistrationForm(string firstName, string lastName, string gender, string mobileNumber, string? email = null, string? dateOfBirth = null, string[]? subjects = null, string[]? hobbies = null, string? picturePath = null, string? currentAddress = null, string? state = null, string? city = null)
        {
            EnterFirstName(firstName);
            EnterLastName(lastName);
            SelectGender(gender);
            EnterMobileNumber(mobileNumber);
            if (!string.IsNullOrWhiteSpace(email))
            {
                EnterEmail(email);
            }
            if (!string.IsNullOrWhiteSpace(dateOfBirth))
            {
                SelectDateOfBirth(dateOfBirth);
            }
            if (subjects != null && subjects.Length > 0)
            {
                SelectSubjects(subjects);
            }
            if (hobbies != null && hobbies.Length > 0)
            {
                SelectHobbies(hobbies);
            }
            if (!string.IsNullOrWhiteSpace(picturePath))
            {
                SelectPicture(picturePath);
            }
            if (!string.IsNullOrWhiteSpace(currentAddress))
            {
                EnterCurrentAddress(currentAddress);
            }
            if (!string.IsNullOrWhiteSpace(state))
            {
                SelectState(state);
            }
            if (!string.IsNullOrWhiteSpace(city))
            {
                SelectCity(city);
            }
        }

        public void SubmitRegistrationForm()
        {
            ClickSubmitButton();
        }

        public void RegisterStudent(string firstName, string lastName, string gender, string mobileNumber, string? email = null, string? dateOfBirth = null, string[]? subjects = null, string[]? hobbies = null, string? picturePath = null, string? currentAddress = null, string? state = null, string? city = null)
        {
            FillRegistrationForm(
                firstName,
                lastName,
                gender,
                mobileNumber,
                email,
                dateOfBirth,
                subjects,
                hobbies,
                picturePath,
                currentAddress,
                state,
                city
            );

            SubmitRegistrationForm();
        }
        public bool IsSubmittedModalDisplayed()
        {
            return _submittedmodal.IsElementDisplayed()
                && _submittedmodalTitle.IsElementDisplayed();
        }

        public string GetSubmittedValue(string label)
        {
            return SubmittedValueByLabel(label).GetText();
        }

    }
}