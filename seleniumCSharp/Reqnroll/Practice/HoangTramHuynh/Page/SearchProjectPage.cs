using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HoangTramHuynh.Page
{
    public class SearchProjectPage
    {
        private readonly WebObject _searchType = new WebObject(By.CssSelector("select#type"), "Search Type");
        private readonly WebObject _projectName = new WebObject(By.XPath("//span[@ui-view='project']//input[@ng-model='input.projectname']"), "Project Name Input");
        private readonly WebObject _projectType = new WebObject(By.CssSelector("select#ddl-projecttype"), "Project Type Dropdown");
        private readonly WebObject _location = new WebObject(By.CssSelector("select#ddl-location"), "Location Dropdown");
        private readonly WebObject _searchButton = new WebObject(By.XPath("//span[@ui-view='project']//button[@ng-click='search(input)']"), "Search Button");
        private readonly WebObject _resultTable = new WebObject(By.XPath("//div[@ui-view='projectsresult']//table"), "Search Result Table");
        private readonly By _resultRowsLocator = By.XPath("//div[@ui-view='projectsresult']//table//tbody//tr");
        private readonly WebObject _noResultMessage = new WebObject(By.XPath("//div[@ui-view='projectsresult']//div[@id='div-no-result-content']//label"), "No Result Message");

        private WebObject OptionByText(string selectId, string optionText)
        {
            return new WebObject(
                By.XPath($"//select[@id='{selectId}']/option[normalize-space(.)='{optionText}']"),
                $"{optionText} Option"
            );
        }

         public bool IsSearchProjectPageDisplayed()
        {
            return BrowserFactory.GetWebDriver().Url.Contains("#!/search");
        }

        public bool IsSearchTypeProjectSelected()
        {
            SelectElement select = new SelectElement(_searchType.WaitForElementToBeVisible());

            return select.SelectedOption.Text.Trim().Equals("Project", StringComparison.OrdinalIgnoreCase);
        }
        public void EnsureSearchTypeIsProject()
        {
            if (!IsSearchTypeProjectSelected())
            {
                OptionByText("type", "Project").ClickOnElement();
            }
        }

        public void EnterProjectName(string projectName)
        {
            if (!string.IsNullOrWhiteSpace(projectName))
            {
                _projectName.EnterText(projectName);
            }
        }

        public void SelectProjectType(string projectType)
        {
            SelectOption("ddl-projecttype", projectType);
        }

        public void SelectLocation(string location)
        {
            SelectOption("ddl-location", location);
        }

        private void SelectOption(string selectId, string optionText)
        {
            if (!string.IsNullOrWhiteSpace(optionText))
            {
                OptionByText(selectId, optionText).ClickOnElement();
            }
        }

        public string SelectRandomProjectType()
        {
            return _projectType.SelectRandomOption();
        }

        public string SelectRandomLocation()
        {
            return _location.SelectRandomOption();
        }

        public void ClickSearchButton()
        {
            _searchButton.ClickOnElement();
        }

        public void SearchProject(string projectName, string projectType, string location)
        {
            EnsureSearchTypeIsProject();
            EnterProjectName(projectName);
            SelectProjectType(projectType);
            SelectLocation(location);
            ClickSearchButton();
        }

        public bool WaitUntilResultOrNoResultDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(30));

            return wait.Until(driver =>
                driver.FindElements(_resultRowsLocator).Any(row => row.Displayed)|| driver.FindElements(_noResultMessage.By).Any(message => message.Displayed)
            );
        }

        public bool IsSearchResultDisplayed()
        {
            return BrowserFactory.GetWebDriver()
                .FindElements(_resultRowsLocator)
                .Any(row => row.Displayed);
        }

        public bool IsNoResultMessageDisplayed()
        {
            return BrowserFactory.GetWebDriver()
                .FindElements(_noResultMessage.By)
                .Any(message => message.Displayed);
        }

        public string GetNoResultMessage()
        {
            return _noResultMessage.GetText();
        }

        public bool AreAllProjectResultsMatched(string projectName, string projectType, string location)
        {
            List<IWebElement> rows = _resultTable.GetRows();

            return rows.Count > 0
                && rows.All(row =>
                    IsMatched(row, "Project Name", projectName, true)
                    && IsMatched(row, "Project Type", projectType)
                    && IsMatched(row, "Location", location)
                );
        }

        private bool IsMatched(IWebElement row,string columnName,string expected,bool contains = false)
        {
            if (string.IsNullOrWhiteSpace(expected)
                || expected.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            string actual = Normalize(
                _resultTable.GetCellValue(row, columnName)
            );

            expected = Normalize(expected);

            return contains
                ? actual.Contains(expected, StringComparison.OrdinalIgnoreCase)
                : actual.Equals(expected, StringComparison.OrdinalIgnoreCase);
        }

        private string Normalize(string text)
        {
            return text.Trim()
                .Replace("\r", "")
                .Replace("\n", " ")
                .Replace("  ", " ");
        }
    }
}