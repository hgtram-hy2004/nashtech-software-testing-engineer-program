using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HuynhHoangTram.Core;
using HuynhHoangTram.DataObject;

namespace HuynhHoangTram.Page
{
    public class ProjectDetailsPage
    {
        private WebObject _btnEdit = new WebObject(By.CssSelector("button#btnEdit"), "Edit Button");

        private WebObject _btnDelete = new WebObject(By.CssSelector("button#btnDelete"), "Delete Button");

        private WebObject ProjectValueByLabel(string labelName)
        {
            return new WebObject(By.XPath($"//label[contains(normalize-space(.),'{labelName}')]/following-sibling::p[1]"), $"{labelName} Value");
        }

        public bool IsProjectDetailsPageDisplayed()
        {
            return _btnEdit.IsElementDisplayed()
                && _btnDelete.IsElementDisplayed();
        }

        public string GetProjectName()
        {
            return ProjectValueByLabel("Project Name").GetText();
        }

        public string GetProjectType()
        {
            return ProjectValueByLabel("Project Type").GetText();
        }

        public string GetProjectStatus()
        {
            return ProjectValueByLabel("Project Status").GetText();
        }

        public string GetStartDate()
        {
            return ProjectValueByLabel("Start Date").GetText();
        }

        public string GetEndDate()
        {
            return ProjectValueByLabel("End Date").GetText();
        }

        public string GetSizeDay()
        {
            return ProjectValueByLabel("Size").GetText();
        }

        public string GetLocation()
        {
            return ProjectValueByLabel("Location").GetText();
        }

        public string GetProjectManager()
        {
            return ProjectValueByLabel("Project Manager").GetText();
        }

        public string GetDeliveryManager()
        {
            return ProjectValueByLabel("Delivery").GetText();
        }

        public string GetEngagementManager()
        {
            return ProjectValueByLabel("Engagement Manager").GetText();
        }

        public string GetShortDescription()
        {
            return ProjectValueByLabel("Short Description").GetText();
        }

        public string GetLongDescription()
        {
            return ProjectValueByLabel("Long Description").GetText();
        }

        public string GetTechnologies()
        {
            return ProjectValueByLabel("Technologies").GetText();
        }

        public string GetClientName()
        {
            return ProjectValueByLabel("Client Name").GetText();
        }

        public string GetClientIndustry()
        {
            return ProjectValueByLabel("Client Industry").GetText();
        }

        public string GetClientDescription()
        {
            return ProjectValueByLabel("Client Description").GetText();
        }

        public bool IsProjectInformationCorrect(ProjectData projectData)
        {
            return IsTextEqual(GetProjectName(), projectData.ProjectName)
                && IsTextEqual(GetProjectType(), projectData.ProjectType)
                && IsTextEqual(GetProjectStatus(), projectData.ProjectStatus)
                && IsTextContains(GetStartDate(), projectData.StartDate)
                && IsTextContains(GetEndDate(), projectData.EndDate)
                && IsTextContains(GetSizeDay(), projectData.SizeDay)
                && IsTextEqual(GetLocation(), projectData.Location)
                && IsTextContains(GetProjectManager(), GetNameOnly(projectData.ProjectManager))
                && IsTextContains(GetDeliveryManager(), GetNameOnly(projectData.DeliveryManager))
                && IsTextContains(GetEngagementManager(), GetNameOnly(projectData.EngagementManager))
                && IsTextEqual(GetShortDescription(), projectData.ShortDescription)
                && IsTextEqual(GetLongDescription(), projectData.LongDescription)
                && IsTextEqual(GetTechnologies(), projectData.Technologies)
                && IsTextEqual(GetClientName(), projectData.ClientName)
                && IsOptionalTextEqual(GetClientIndustry(), projectData.ClientIndustry)
                && IsOptionalTextEqual(GetClientDescription(), projectData.ClientDescription);
        }

        private string GetNameOnly(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }

            int index = value.IndexOf("(");

            return index > 0
                ? value.Substring(0, index).Trim()
                : value.Trim();
        }

        private bool IsTextEqual(string actual, string expected)
        {
            return NormalizeText(actual).Equals(
                NormalizeText(expected),
                StringComparison.OrdinalIgnoreCase
            );
        }

        private bool IsTextContains(string actual, string expected)
        {
            if (string.IsNullOrWhiteSpace(expected))
            {
                return true;
            }

            return NormalizeText(actual).Contains(
                NormalizeText(expected),
                StringComparison.OrdinalIgnoreCase
            );
        }

        private bool IsOptionalTextEqual(string actual, string expected)
        {
            if (string.IsNullOrWhiteSpace(expected))
            {
                return true;
            }

            return IsTextEqual(actual, expected);
        }

        private string NormalizeText(string text)
        {
            return text
                .Trim()
                .Replace("\r", "")
                .Replace("\n", " ")
                .Replace("  ", " ");
        }
    }
}