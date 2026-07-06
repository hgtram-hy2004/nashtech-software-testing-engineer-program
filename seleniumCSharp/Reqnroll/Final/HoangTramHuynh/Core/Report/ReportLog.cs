using System.Text;
using AventStack.ExtentReports.MarkupUtils;
using HoangTramHuynh.Models.Response;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HoangTramHuynh.Core.Report
{
    public class ReportLog
    {
        public static void Info(string message)
        {
            ExtentReport.LogInfo(message);
        }
    }
}