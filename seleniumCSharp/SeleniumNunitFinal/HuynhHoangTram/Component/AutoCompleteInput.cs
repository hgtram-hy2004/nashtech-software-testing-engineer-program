using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using OpenQA.Selenium;

namespace HuynhHoangTram.Component
{
    public class AutoCompleteInput
    {
        private readonly WebObject _input;
        private readonly WebObject _dropdownList;
        private readonly Func<string, WebObject> _dropdownOption;

        public AutoCompleteInput(
            WebObject input,
            WebObject dropdownList,
            Func<string, WebObject> dropdownOption
        )
        {
            _input = input;
            _dropdownList = dropdownList;
            _dropdownOption = dropdownOption;
        }

        public void TypeTextAndSelectOption(string text, string optionText)
        {
            EnterText(text);
            WaitUntilDropdownDisplayed();
            SelectOption(optionText);
        }

        public void EnterText(string text)
        {
            _input.EnterText(text);
        }

        public void WaitUntilDropdownDisplayed()
        {
            _dropdownList.WaitForElementToBeVisible();
        }

        public void SelectOption(string optionText)
        {
            _dropdownOption(optionText).ClickOnElement();
        }

    }
}