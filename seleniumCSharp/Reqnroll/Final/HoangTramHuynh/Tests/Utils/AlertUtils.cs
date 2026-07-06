using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Core.UI;
using OpenQA.Selenium;

namespace HoangTramHuynh.Utils
{
    public class AlertUtils
    {
        public static string AcceptAlertAndGetText()
        {
            IAlert alert = WebObjectExtension.WaitForAlert();
            ReportLog.Info($"Alert text: {alert.Text}");

            string alertText = alert.Text;
            alert.Accept();
            ReportLog.Info("Alert accepted.");

            return alertText;
        }
    }
}