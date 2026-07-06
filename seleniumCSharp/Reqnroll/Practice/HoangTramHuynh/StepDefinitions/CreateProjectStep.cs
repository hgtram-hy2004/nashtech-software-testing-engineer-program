using System;
using Reqnroll;
using HoangTramHuynh.Core;
using HoangTramHuynh.Utils;
using HoangTramHuynh.Page;
using HoangTramHuynh.DataObject;
	
	namespace HoangTramHuynh.StepDefinitions
	{
		[Binding]
		public class CreateProjectStep
		{
			private readonly ScenarioContext _scenarioContext;
        	private readonly HomePage _homePage = new HomePage();
        	private readonly CreateProjectPage _createProjectPage = new CreateProjectPage();
			private readonly ProjectDetailsPage _projectDetailsPage = new ProjectDetailsPage();
			private ProjectData _projectData = new ProjectData();
			public CreateProjectStep(ScenarioContext scenarioContext)
			{
				_scenarioContext = scenarioContext;
			}
			// This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
			[Given("the user navigates to Create Project page")]
			public void GivenTheUserNavigatesToCreateProjectPage()
			{
				_homePage.NavigateToCreateProject();
			}
			// This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
			[When("the user fills in all project information")]
			public void WhenTheUserFillsInAllProjectInformation()
			{
				ProjectData projectData = JsonUtils.ReadJsonFile<ProjectData>("TestData\\CreateProjectData.json");
				string uniqueText = DateTime.Now.ToString("yyyyMMddHHmmss");
				projectData.ProjectName = projectData.ProjectName + "_" + uniqueText;
				_projectData = projectData;
				_createProjectPage.FillCreateProjectForm(_projectData);
			}
			
			// This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
			[When("the user clicks Create button")]
			public void WhenTheUserClicksCreateButton()
			{
				_createProjectPage.ClickCreateButton();
			}
			// This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
			[Then("all information of the project is shown correctly")]
			public void ThenAllInformationOfTheProjectIsShownCorrectly()
			{
				Assert.That(
					_projectDetailsPage.WaitUntilProjectDetailUrlDisplayed(),
					Is.True,
					"Project detail URL is not displayed after creating project."
				);

				Assert.That(
					BrowserFactory.GetWebDriver().Url,
					Does.Contain("#!/project/"),
					"Current URL is not Project Detail page."
				);

				Assert.That(
					_projectDetailsPage.IsProjectDetailsPageDisplayed(),
					Is.True,
					"Project Details page is not displayed."
				);

				Assert.That(
					_projectDetailsPage.IsProjectInformationCorrect(_projectData),
					Is.True,
					"Project information on detail page is not correct."
				);

			}

		}
	}