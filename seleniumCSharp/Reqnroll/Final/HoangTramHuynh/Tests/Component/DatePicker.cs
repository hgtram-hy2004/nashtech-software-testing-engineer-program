using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Globalization;
using OpenQA.Selenium.Support.UI;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Core.UI;

namespace HoangTramHuynh.Component
{
    public class DatePicker
    {
        private readonly WebObject _dateInput;
        private readonly WebObject _calendarContainer;
        private readonly WebObject _yearDropdown;
        private readonly WebObject _monthDropdown;
        private readonly Func<string, WebObject> _monthOption;
        private readonly Func<string, WebObject> _yearOption;
        private readonly Func<string, WebObject> _dayOption;

        public DatePicker(WebObject dateInput, WebObject calendar, WebObject yearDropdown, WebObject monthDropdown, Func<string, WebObject> yearOption, Func<string, WebObject> monthOption, Func<string, WebObject> dayOption)
        {
            _dateInput = dateInput;
            _calendarContainer = calendar;
            _monthDropdown = monthDropdown;
            _yearDropdown = yearDropdown;
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
            ReportLog.Info("Date picker is opened.");
        }

        public void SelectYear(string year)
        {
            _calendarContainer.WaitForElementToBeVisible();
            _yearDropdown.ClickDropdownOptionByMouse(_yearOption(year));
            ReportLog.Info($"Selected year '{year}' from year dropdown.");
        }

        public void SelectMonth(string monthValue)
        {
            _calendarContainer.WaitForElementToBeVisible();
            _monthDropdown.ClickDropdownOptionByMouse(_monthOption(monthValue));
            ReportLog.Info($"Selected month with value '{monthValue}' from month dropdown.");
        }

        public void SelectDay(string day)
        {
            _dayOption(day).ClickOnElement();
            ReportLog.Info($"Selected day '{day}' from calendar.");
        }
        private DateTime ConvertToDate(string date)
        {
            string[] formats =
            {
                "dd-MMM-yyyy",
                "d-MMM-yyyy",
                "dd MMM yyyy",
                "d MMM yyyy",
                "dd MMMM yyyy",
                "d MMMM yyyy",
                "dd MMMM,yyyy",
                "d MMMM,yyyy"
            };
            return DateTime.ParseExact(date,formats,CultureInfo.InvariantCulture,DateTimeStyles.None);
        }
        public static bool IsDateMatched(string actual, string? expected)
        {
            if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(expected))
            {
                return false;
            }

            string normalizedActual = WebObjectExtension.NormalizeText(actual)
                .Replace(" ", "")
                .Replace("-", "")
                .Replace(",", "");

            string normalizedExpected = WebObjectExtension.NormalizeText(expected)
                .Replace(" ", "")
                .Replace("-", "")
                .Replace(",", "");

            return normalizedActual.Contains(normalizedExpected,StringComparison.OrdinalIgnoreCase);
        }
    }
}