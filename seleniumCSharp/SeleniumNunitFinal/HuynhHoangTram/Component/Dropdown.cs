using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using OpenQA.Selenium;
namespace HuynhHoangTram.Component
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
        }

        public void WaitUntilDropdownDisplayed()
        {
            _dropdownList.WaitForElementToBeVisible();
        }

        public void ClickOption(string optionText)
        {
            _option(optionText).ClickOnElement();
        }
        
    }
}