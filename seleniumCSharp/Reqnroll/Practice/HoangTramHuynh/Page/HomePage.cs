using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core;
using OpenQA.Selenium;
namespace HoangTramHuynh.Page
{
    public class HomePage
    {
        private WebObject _dropdownproject = new WebObject(By.XPath("//a[contains(.,'Projects')]"), "Dropdown Project");
        private WebObject _createproject = new WebObject(By.CssSelector("a[data-target='#modalCreateProject']"), "Create Project Option");
        private WebObject _searchproject = new WebObject(By.XPath("//a[contains(.,'Search Project')]"), "Search Project Option");
        private WebObject _avatar = new WebObject(By.CssSelector("img#ava"), "Avatar account login");
        private WebObject accountlogininformation(string username)
        {
            return new WebObject(
                By.XPath($"//img[@id='ava']/ancestor::a[contains(normalize-space(.),'{username}')]"),
                "Account Login Information"
            );
        }
        public bool IsHomePageDisplayed()
        {
            return _avatar.IsElementDisplayed();
        }

        public bool IsLoginWithCorrectAccount(string username)
        {
            return accountlogininformation(username).IsElementDisplayed();
        }

        public bool IsProjectDropdownDisplayed()
        {
            return _dropdownproject.IsElementDisplayed();
        }

        public bool IsCreateProjectOptionDisplayed()
        {
            return _createproject.IsElementDisplayed();
        }

        public bool IsSearchProjectOptionDisplayed()
        {
            return _searchproject.IsElementDisplayed();
        }

        public void ClickProjectDropdown()
        {
            _dropdownproject.ClickOnElement();
        }

        public void ClickCreateProject()
        {
            _createproject.ClickOnElement();
        }

        public void ClickSearchProject()
        {
            _searchproject.ClickOnElement();
        }


        public void NavigateToCreateProject()
        {
            ClickProjectDropdown();

            ClickCreateProject();
        }

        public void NavigateToSearchProject()
        {
            ClickProjectDropdown();

            ClickSearchProject();
        }

    }
}