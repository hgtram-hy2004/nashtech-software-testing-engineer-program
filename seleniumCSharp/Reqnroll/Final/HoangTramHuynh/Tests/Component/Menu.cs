using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;
namespace HoangTramHuynh.Component
{
    public class Menu
    {
        private readonly Func<string, WebObject>? _menuItem;
        private readonly Func<string, WebObject>? _parentMenu;
        private readonly Func<string, WebObject>? _childMenu;

        public Menu(Func<string, WebObject> menuItem)
        {
            _menuItem = menuItem;
        }

        public Menu(Func<string, WebObject> parentMenu, Func<string, WebObject> childMenu)
        {
            _parentMenu = parentMenu;
            _childMenu = childMenu;
        }

        public void SelectMenuItem(string menuPath)
        {
            string[] menus = menuPath
                .Split("->")
                .Select(menu => menu.Trim())
                .ToArray();

            if (menus.Length == 1)
            {
                ClickSingleMenu(menus[0]);

                return;
            }

            ClickParentMenu(menus[0]);

            ClickChildMenu(menus[1]);
            ReportLog.Info($"Selected menu item '{menuPath}'.");
        }

        private void ClickSingleMenu(string menuText)
        {
            if (_menuItem != null)
            {
                _menuItem(menuText).ClickOnElement();

                return;
            }
            _childMenu!(menuText).ClickOnElement();
            ReportLog.Info($"Clicked on menu '{menuText}'.");
        }

        private void ClickParentMenu(string menuText)
        {
            _parentMenu!(menuText).ClickOnElement();
            ReportLog.Info($"Clicked on parent menu '{menuText}'.");
        }

        private void ClickChildMenu(string menuText)
        {
            _childMenu!(menuText).ClickOnElement();
            ReportLog.Info($"Clicked on child menu '{menuText}'.");
        }
    }
}