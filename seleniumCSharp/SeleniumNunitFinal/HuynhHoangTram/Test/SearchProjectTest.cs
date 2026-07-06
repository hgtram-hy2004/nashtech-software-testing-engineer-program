using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Page;

namespace HuynhHoangTram.Test
{
    public class SearchProjectTest : BaseTest
    {
        LoginPage loginPage = new LoginPage();
        HomePage homePage = new HomePage();
        SearchProjectPage searchProjectPage = new SearchProjectPage();
        private string GetRandomText(int length = 10)
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();

            return new string(Enumerable.Range(0, length)
            .Select(_ => letters[random.Next(letters.Length)])
            .ToArray());
        }
        private void LoginAndGoToSearchProjectPage()
        {
            loginPage.Login("Admin2", "Fxu1tw^E");

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

            homePage.NavigateToSearchProject();

            Assert.That(
                searchProjectPage.IsSearchProjectPageDisplayed(),
                Is.True,
                "Search Project page is not displayed."
            );

            searchProjectPage.EnsureSearchTypeIsProject();

            Assert.That(
                searchProjectPage.IsSearchTypeProjectSelected(),
                Is.True,
                "Search type is not Project."
            );
        }

        [Test]
        public void SearchProjectSuccessfully()
        {
            LoginAndGoToSearchProjectPage();

            string randomLetter = GetRandomText(1);

            searchProjectPage.EnterProjectName(randomLetter);

            string selectedProjectType = searchProjectPage.SelectRandomProjectType();

            string selectedLocation = searchProjectPage.SelectRandomLocation();

            searchProjectPage.ClickSearchButton();

            Assert.That(searchProjectPage.WaitUntilResultOrNoResultDisplayed(),
                Is.True,
                "Neither search result nor no result message is displayed."
            );

            bool isResultDisplayed = searchProjectPage.IsSearchResultDisplayed();

            bool isNoResultMessageDisplayed = searchProjectPage.IsNoResultMessageDisplayed();

            Assert.That(isResultDisplayed || isNoResultMessageDisplayed,
                Is.True,
                "Neither search result nor no result message is displayed."
            );

            if (isResultDisplayed)
            {
                Assert.That(searchProjectPage.AreAllProjectResultsMatched(randomLetter, selectedProjectType, selectedLocation),
                    Is.True,
                    "Some project results do not match the random search criteria."
                );

                TestContext.Progress.WriteLine("All search results match random criteria.");
            }
            else
            {
                Assert.That(searchProjectPage.GetNoResultMessage(), Does.Contain("No result were found to match your search"));

                TestContext.Progress.WriteLine("No result message is displayed with random data.");
            }

            TestContext.Progress.WriteLine("Search Name: " + randomLetter);

            TestContext.Progress.WriteLine("Selected Project Type: " + selectedProjectType);

            TestContext.Progress.WriteLine("Selected Location: " + selectedLocation);
        }

    }
}