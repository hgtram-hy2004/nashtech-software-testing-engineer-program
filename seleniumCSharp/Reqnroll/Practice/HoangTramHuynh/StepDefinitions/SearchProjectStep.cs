using System;
using Reqnroll;
using HoangTramHuynh.Core;
using HoangTramHuynh.Utils;
using HoangTramHuynh.Page;
	
	namespace HoangTramHuynh.StepDefinitions
	{
		[Binding]
		public class SearchProjectStep
		{
			private readonly ScenarioContext _scenarioContext;
            private readonly HomePage _homePage = new HomePage();
            private readonly SearchProjectPage _searchProjectPage = new SearchProjectPage();

            private string _searchName = string.Empty;
            private string _selectedProjectType = string.Empty;
            private string _selectedLocation = string.Empty;
	
			public SearchProjectStep(ScenarioContext scenarioContext)
			{
				_scenarioContext = scenarioContext;
			}
            [Given("the user navigates to Search Project page")]
            public void GiventheusernavigatestoSearchProjectpage()
            {
                _homePage.NavigateToSearchProject();

                Assert.That(
                    _searchProjectPage.IsSearchProjectPageDisplayed(),
                    Is.True,
                    "Search Project page is not displayed."
                );

                _searchProjectPage.EnsureSearchTypeIsProject();

                Assert.That(
                    _searchProjectPage.IsSearchTypeProjectSelected(),
                    Is.True,
                    "Search type is not Project."
                );
            }
            [When("the user applies search criteria by Name, Location, or Type")]
            public void WhenTheUserAppliesSearchCriteriaByNameLocationOrType()
            {
                _searchName = GetRandomText(1);

                _searchProjectPage.EnterProjectName(_searchName);

                _selectedProjectType = _searchProjectPage.SelectRandomProjectType();

                _selectedLocation = _searchProjectPage.SelectRandomLocation();
            }
            [When("the user clicks on Search button")]
            public void WhenTheUserClicksOnSearchButton()
            {
                _searchProjectPage.ClickSearchButton();
            }
            [Then("all projects matched with the input criteria will be displayed")]
            public void ThenAllProjectsMatchedWithTheInputCriteriaWillBeDisplayed()
            {
                Assert.That(
                _searchProjectPage.WaitUntilResultOrNoResultDisplayed(),
                Is.True,
                "Neither search result nor no result message is displayed."
                );

                bool isResultDisplayed = _searchProjectPage.IsSearchResultDisplayed();
                bool isNoResultMessageDisplayed = _searchProjectPage.IsNoResultMessageDisplayed();

                Assert.That(
                    isResultDisplayed || isNoResultMessageDisplayed,
                    Is.True,
                    "Neither search result nor no result message is displayed."
                );

                if (isResultDisplayed)
                {
                    Assert.That(
                        _searchProjectPage.AreAllProjectResultsMatched(
                            _searchName,
                            _selectedProjectType,
                            _selectedLocation
                        ),
                        Is.True,
                        "Some project results do not match the search criteria."
                    );

                    TestContext.Progress.WriteLine("All search results match criteria.");
                }
                else
                {
                    Assert.That(
                        _searchProjectPage.GetNoResultMessage(),
                        Does.Contain("No result were found to match your search"),
                        "No result message is not correct."
                    );
                }
            }
            private string GetRandomText(int length = 10)
            {
                string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                Random random = new Random();

                return new string(
                    Enumerable.Range(0, length)
                        .Select(_ => letters[random.Next(letters.Length)])
                        .ToArray()
                );
            }

		}
	}