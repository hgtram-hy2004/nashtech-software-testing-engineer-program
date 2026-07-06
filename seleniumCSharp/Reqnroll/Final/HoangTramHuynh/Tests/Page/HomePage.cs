using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HoangTramHuynh.Component;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;
using OpenQA.Selenium.Interactions;

namespace HoangTramHuynh.Page
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
            return new WebObject(By.XPath($"//div[contains(@class,'category-cards')]//a[.//h5[normalize-space()='{menuText}']]"),menuText + " Home Menu Item");
        }

        public void NavigateToForms()
        {
            _homeMenu.SelectMenuItem("Forms");
            ReportLog.Info("Navigated to Forms page.");
        }
        public void NavigateToBook()
        {
            WebObject bookStoreCard = HomeMenuItem("Book Store Application");
            bookStoreCard.ClickOnElement();
            ReportLog.Info("Navigated to Book Store page.");
        }

    }
}