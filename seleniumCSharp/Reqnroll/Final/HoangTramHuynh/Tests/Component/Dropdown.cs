using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;
using OpenQA.Selenium;
namespace HoangTramHuynh.Component
{
    public class Dropdown
    {
        private readonly WebObject _dropdown;
        private readonly WebObject _dropdownList;
        private readonly Func<string, WebObject> _option;

        public Dropdown(WebObject dropdown, WebObject dropdownList, Func<string, WebObject> option)
        {
            _dropdown = dropdown;
            _dropdownList = dropdownList;
            _option = option;
        }

        public void SelectOption(string optionText)
        {
            OpenDropdown();
            WaitUntilDropdownDisplayed();
            ClickOption(optionText);
        }

        public void OpenDropdown()
        {
            _dropdown.ScrollToElement();
            _dropdown.ClickOnElement();
            ReportLog.Info("Dropdown is opened.");
        }

        public void WaitUntilDropdownDisplayed()
        {
            _dropdownList.WaitForElementToBeVisible();
            ReportLog.Info("Dropdown options are displayed.");
        }

        public void ClickOption(string optionText)
        {
            _option(optionText).ClickOnElement();
            ReportLog.Info($"Selected option '{optionText}' from dropdown.");
        }
        
    }
}