using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core;
using HoangTramHuynh.DataObject;
using OpenQA.Selenium;
namespace HoangTramHuynh.Page
{
    public class CreateProjectPage
    {
        private WebObject _modalCreateProject = new WebObject(By.CssSelector("div#modalCreateProject"), "Create Project Modal");
        private WebObject _projectname = new WebObject(By.CssSelector("input#name"), "Project Name Input");
        private WebObject _startdate = new WebObject(By.CssSelector("input[name='sdate']"), "Project Start Date Select");
        private WebObject _enddate = new WebObject(By.CssSelector("input[name='edate']"), "Project End Date Select");
        private WebObject _sizeday = new WebObject(By.CssSelector("input#sizeday"), "Size Day Input");
        private WebObject _shortdescription = new WebObject(By.CssSelector("textarea#shortDescription"), "Short Description Input");
        private WebObject _longdescription = new WebObject(By.CssSelector("textarea#longDescription"), "Long Description Input");
        private WebObject _technologies = new WebObject(By.CssSelector("textarea#technologies"), "Technologies Input");
        private WebObject _clientname = new WebObject(By.CssSelector("input#clientName"), "Client Industry Input");
        private WebObject _clientdescription = new WebObject(By.CssSelector("textarea#clientdescription"), "Client Description Input");
        private WebObject _btncreate = new WebObject(By.CssSelector("button#btnConfirm"), "Create Button");
        private WebObject _btncancel = new WebObject(By.XPath("//button[@ng-click='pcC.cancel()']"), "Cancel Button");
        public bool IsCreateProjectModalDisplayed()
        {
            return _modalCreateProject.IsElementDisplayed();
        }

        public bool IsCreateButtonDisplayed()
        {
            return _btncreate.IsElementDisplayed();
        }

        public bool IsCancelButtonDisplayed()
        {
            return _btncancel.IsElementDisplayed();
        }

        public void EnterProjectName(string projectName)
        {
            _projectname.EnterText(projectName);
        }

        public void EnterStartDate(string startDate)
        {
            _startdate.SetTextByJavaScript(startDate);
        }

        public void EnterEndDate(string endDate)
        {
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                _enddate.SetTextByJavaScript(endDate);
            }
        }

        public void EnterSizeDay(string sizeDay)
        {
            _sizeday.EnterText(sizeDay);
        }
        public void EnterShortDescription(string shortDescription)
        {
            _shortdescription.EnterText(shortDescription);
        }

        public void EnterLongDescription(string longDescription)
        {
            _longdescription.EnterText(longDescription);
        }

        public void EnterTechnologies(string technologies)
        {
            _technologies.EnterText(technologies);
        }

        public void EnterClientName(string clientName)
        {
            _clientname.EnterText(clientName);
        }

        public void EnterClientDescription(string clientDescription)
        {
            _clientdescription.EnterText(clientDescription);
        }
        private WebObject OptionByText(string selectId, string optionText)
        {
            return new WebObject(By.XPath($"//select[@id='{selectId}']/option[contains(normalize-space(.),'{optionText}')]"), $"{optionText} Option");
        }
        private void SelectOption(string selectId, string optionText)
        {
            WebObject option = OptionByText(selectId, optionText);

            option.ClickOnElement();
        }
        public void ClickCreateButton()
        {
            _btncreate.ClickOnElement();
        }

        public void ClickCancelButton()
        {
            _btncancel.ClickOnElement();
        }
        public void FillCreateProjectForm(ProjectData projectData)
        {
            _projectname.EnterText(projectData.ProjectName);

            SelectOption("projecttype", projectData.ProjectType);

            SelectOption("status", projectData.ProjectStatus);

            _startdate.SetTextByJavaScript(projectData.StartDate);

            _enddate.SetTextByJavaScript(projectData.EndDate);

            _sizeday.EnterText(projectData.SizeDay);

            SelectOption("location", projectData.Location);

            SelectOption("projectManager", projectData.ProjectManager);

            SelectOption("deliveryManager", projectData.DeliveryManager);

            SelectOption("engagementManager", projectData.EngagementManager);

            _shortdescription.EnterText(projectData.ShortDescription);

            _longdescription.EnterText(projectData.LongDescription);

            _technologies.EnterText(projectData.Technologies);

            _clientname.EnterText(projectData.ClientName);

            SelectOption("clientindustry", projectData.ClientIndustry);

            _clientdescription.EnterText(projectData.ClientDescription);
        }

        public void CreateProjectSuccessfully(ProjectData projectData)
        {
            FillCreateProjectForm(projectData);

            ClickCreateButton();
        }
    }
}