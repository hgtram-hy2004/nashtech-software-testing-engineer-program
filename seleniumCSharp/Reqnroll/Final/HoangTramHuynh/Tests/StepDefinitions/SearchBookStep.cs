using HoangTramHuynh.Page;
using HoangTramHuynh.Contexts;
using HoangTramHuynh.DataObject;
using Reqnroll.Assist;
using OpenQA.Selenium;
using Reqnroll;
	
	namespace HoangTramHuynh.StepDefinitions
	{
		[Binding]
		public class SearchBookStep
		{
			private readonly SearchBookContext _searchBookContext;
            private readonly HomePage _homePage = new HomePage();
            private readonly BookStorePage _bookStorePage = new BookStorePage();
			public SearchBookStep(SearchBookContext searchBookContext)
			{
				_searchBookContext = searchBookContext;
			}
			
            // This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
            [Given("the user is on the Book Store page")]
            public void GivenTheUserIsOnTheBookStorePage()
            {
                _homePage.NavigateToBook();
                Assert.That(_bookStorePage.IsBookStorePageDisplayed(),Is.True,"Book Store page is not displayed.");
            }
            // This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
            [When("the user inputs book search data")]
            public void WhenTheUserInputsBookSearchData(Table table)
            {
                _searchBookContext.BookData = table.CreateInstance<BookData>();
                _bookStorePage.SearchBook(_searchBookContext.BookData.BookName);
            }

            // This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
            [Then("all books match with input criteria will be displayed")]
            public void ThenAllBooksMatchWithInputCriteriaWillBeDisplayed()
            {
                Assert.That(_bookStorePage.HasSearchResult(),Is.True,"No book is displayed after searching.");
                Assert.That(_bookStorePage.AreAllBooksMatchedSearchCriteria( _searchBookContext.BookData.BookName),Is.True,$"Some books do not match search criteria '{_searchBookContext.BookData.BookName}'.");
            }

		}
	}