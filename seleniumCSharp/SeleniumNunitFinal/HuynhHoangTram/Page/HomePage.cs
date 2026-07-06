using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HuynhHoangTram.Component;
using HuynhHoangTram.Core;
using OpenQA.Selenium.Interactions;

namespace HuynhHoangTram.Page
{
    public class HomePage
    {
        private readonly Menu _homeMenu;

        public HomePage()
        {
            _homeMenu = new Menu(HomeMenuItem);
        }

        private WebObject HomeMenuItem(string menuText)
        {
            return new WebObject(
                By.XPath($"//div[contains(@class,'category-cards')]//h5[normalize-space(.)='{menuText}']/ancestor::a"),
                menuText + " Home Menu Item");
        }

        public void NavigateToForms()
        {
            _homeMenu.SelectMenuItem("Forms");
        }
        public void NavigateToBook()
        {
            WebObject bookStoreCard = HomeMenuItem("Book Store Application");

            IJavaScriptExecutor js =
                (IJavaScriptExecutor)BrowserFactory.GetWebDriver();

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

            bookStoreCard.ScrollToElement(-250);

            bookStoreCard.ClickOnElement();
        }

    }
}