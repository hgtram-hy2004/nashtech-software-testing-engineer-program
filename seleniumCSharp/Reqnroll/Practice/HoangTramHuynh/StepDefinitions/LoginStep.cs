using System;
using Reqnroll;
using HoangTramHuynh.Core;
using HoangTramHuynh.Utils;
using HoangTramHuynh.Page;
	
	namespace HoangTramHuynh.StepDefinitions
	{
		[Binding]
		public class StepDefinitions
		{
			private readonly ScenarioContext _scenarioContext;
	   		private readonly LoginPage _loginPage = new LoginPage();
			private readonly HomePage _homePage = new HomePage();
			private string _username = string.Empty;
			public StepDefinitions(ScenarioContext scenarioContext)
			{
				_scenarioContext = scenarioContext;
			}
            [Given("the user visits the TMS website")]
            public void GiventheuservisitstheTMSwebsite()
            {
                string testUrl = ConfigurationUtils.GetConfigurationByKey("TestUrl");
                BrowserFactory.GetWebDriver().Navigate().GoToUrl(testUrl);
            }
            [When("the user logs in with username {string} and password {string}")]
            public void WhenTheUserLogsInWithUsernameAndPassword(string username, string password)
            {
                _username = username;
                _loginPage.EnterUsername(username);
				_loginPage.EnterPassword(password);
            }
            [When("the user clicks on Login button")]
            public void WhenTheUserClicksOnLoginButton()
            {
                _loginPage.ClickLogin();
            }
            [Then("the error message will be displayed")]
            public void ThenTheErrorMessageWillBeDisplayed()
            {
                Assert.That(
					_loginPage.IsLoginFailed(),
					Is.True,
					"The error message should be displayed when logging in with invalid account."
				);
            }
            [Then("the user is logged into the system successfully")]
            public void ThenTheUserIsLoggedIntoTheSystemSuccessfully()
            {
                Assert.Multiple(() =>
				{
					Assert.That(
						_homePage.IsHomePageDisplayed(),
						Is.True,
						"Home page should be displayed after login successfully."
					);

					Assert.That(
						_homePage.IsLoginWithCorrectAccount(_username),
						Is.True,
						"Logged-in username should be displayed correctly."
					);
				});
            }
			[Given("the user is logged into the application")]
			public void GivenTheUserIsLoggedIntoTheApplication()
			{
				string testUrl = ConfigurationUtils.GetConfigurationByKey("TestUrl");

				BrowserFactory.GetWebDriver().Navigate().GoToUrl(testUrl);

				_username = "Admin2";

				_loginPage.Login("Admin2", "Fxu1tw^E");

				Assert.Multiple(() =>
				{
					Assert.That(
						_homePage.IsHomePageDisplayed(),
						Is.True,
						"Home page should be displayed after logging in successfully."
					);

					Assert.That(
						_homePage.IsLoginWithCorrectAccount(_username),
						Is.True,
						"Logged-in username should be displayed correctly."
					);
				});
			}

		}
	}