using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Globalization;
using OpenQA.Selenium.Support.UI;
using HuynhHoangTram.Core;

namespace HuynhHoangTram.Component
{
    public class DatePicker
    {
        private readonly WebObject _dateInput;
        private readonly WebObject _calendarContainer;
        private readonly WebObject _yearDropdown;
        private readonly WebObject _monthDropdown;
        private readonly WebObject _nextButton;
        private readonly WebObject _previousButton;
        private readonly Func<string, WebObject> _monthOption;
        private readonly Func<string, WebObject> _yearOption;
        private readonly Func<string, WebObject> _dayOption;

        public DatePicker(WebObject dateInput, WebObject calendar, WebObject yearDropdown, WebObject monthDropdown, WebObject nextButton, WebObject previousButton, Func<string, WebObject> yearOption, Func<string, WebObject> monthOption, Func<string, WebObject> dayOption)
        {
            _dateInput = dateInput;
            _calendarContainer = calendar;
            _monthDropdown = monthDropdown;
            _yearDropdown = yearDropdown;
            _nextButton = nextButton;
            _previousButton = previousButton;
            _yearOption = yearOption;
            _monthOption = monthOption;
            _dayOption = dayOption;
        }

        public void SelectDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return;
            }
            DateTime dateValue = ConvertToDate(date);
            OpenDatePicker();
            SelectYear(dateValue.Year.ToString());
            SelectMonth((dateValue.Month - 1).ToString());
            SelectDay(dateValue.Day.ToString());
        }

        public void OpenDatePicker()
        {
            _dateInput.ClickOnElement();
            _calendarContainer.WaitForElementToBeVisible();
        }

        public void SelectYear(string year)
        {
            _calendarContainer.WaitForElementToBeVisible();
            _yearDropdown.ClickDropdownOptionByMouse(_yearOption(year));
        }

        public void SelectMonth(string monthValue)
        {
            _calendarContainer.WaitForElementToBeVisible();
            _monthDropdown.ClickDropdownOptionByMouse(_monthOption(monthValue));
        }

        public void SelectDay(string day)
        {
            _dayOption(day).ClickOnElement();
        }

        public void ClickNextButton()
        {
            _nextButton.ClickOnElement();
        }

        public void ClickPreviousButton()
        {
            _previousButton.ClickOnElement();
        }

        public void MoveToNextMonth(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                ClickNextButton();
            }
        }

        public void MoveToPreviousMonth(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                ClickPreviousButton();
            }
        }

        private DateTime ConvertToDate(string date)
        {
            string[] formats =
            {
                "dd-MMMM-yyyy",
                "dd-MM-yyyy",
                "dd MMM yyyy",
                "dd MMMM yyyy"
            };

            return DateTime.ParseExact(
                date,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None
            );
        }
        public static bool IsDateMatched(string actual, string expected)
        {
            string normalizedActual = WebObjectExtension.NormalizeText(actual)
                .Replace(" ", "")
                .Replace("-", "")
                .Replace(",", "");

            string normalizedExpected = WebObjectExtension.NormalizeText(expected)
                .Replace(" ", "")
                .Replace("-", "")
                .Replace(",", "");
            return normalizedActual.Contains(
                normalizedExpected,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}