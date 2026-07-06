using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using HuynhHoangTram.Page;
using HuynhHoangTram.DataObject;
using HuynhHoangTram.Utils;
using HuynhHoangTram.Core;

namespace HuynhHoangTram.Test
{
    public class CreateProjectTest : BaseTest
    {
        CreateProjectPage createprojectPage = new CreateProjectPage();
        HomePage homePage = new HomePage();
        LoginPage loginPage = new LoginPage();
        ProjectDetailsPage projectDetailsPage = new ProjectDetailsPage();

        [Test]
        public void CreateProjectWithAllFieldsSuccessfully()
        {
            List<ProjectData> projectDataList =
                JsonUtils.ReadJsonFile<List<ProjectData>>(
                    "TestData\\CreateProjectData.json"
                );

            Random random = new Random();

            ProjectData projectData =
                projectDataList[random.Next(projectDataList.Count)];

            loginPage.Login(
                "Admin2",
                "Fxu1tw^E"
            );

            Assert.That(
                homePage.IsHomePageDisplayed(),
                Is.True,
                "Home page is not displayed after login."
            );

            Assert.That(
                homePage.IsLoginWithCorrectAccount("Admin2"),
                Is.True,
                "Login account is not correct."
            );

            homePage.NavigateToCreateProject();

            Assert.That(
                createprojectPage.IsCreateProjectModalDisplayed(),
                Is.True,
                "Create Project modal is not displayed."
            );

            createprojectPage.FillCreateProjectForm(projectData);

            createprojectPage.ClickCreateButton();

            Assert.That(
                WaitUntilProjectDetailUrlDisplayed(),
                Is.True,
                "Project detail URL is not displayed after creating project."
            );

            Assert.That(BrowserFactory.GetWebDriver().Url,
                        Does.Contain("#!/project/"),
                        "Current URL is not Project Detail page.");

            Assert.That(
                projectDetailsPage.IsProjectDetailsPageDisplayed(),
                Is.True,
                "Project Details page is not displayed."
            );

            Assert.That(
                projectDetailsPage.IsProjectInformationCorrect(projectData),
                Is.True,
                "Project information on detail page is not correct."
            );

            TestContext.Progress.WriteLine("Created Project Name: " + projectData.ProjectName);

            TestContext.Progress.WriteLine("Project Detail URL: " + BrowserFactory.GetWebDriver().Url);
        }
        private bool WaitUntilProjectDetailUrlDisplayed()
        {
            WebDriverWait wait =new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(30));
            return wait.Until(driver => driver.Url.Contains("#!/project/"));
        }
    }

}