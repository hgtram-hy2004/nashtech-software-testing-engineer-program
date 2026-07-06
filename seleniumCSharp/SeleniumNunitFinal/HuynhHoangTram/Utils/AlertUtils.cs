using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using OpenQA.Selenium;

namespace HuynhHoangTram.Utils
{
    public class AlertUtils
    {
        public static string AcceptAlertAndGetText()
        {
            IAlert alert = WebObjectExtension.WaitForAlert();

            string alertText = alert.Text;

            alert.Accept();

            return alertText;
        }
    }
}