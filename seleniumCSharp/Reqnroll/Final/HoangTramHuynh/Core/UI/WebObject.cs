using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace HoangTramHuynh.Core.UI
{
    public class WebObject
    {
        public By By { get; set; }

        public string Name { get; set; }

        public WebObject(By by, string name = "")
        {
            By = by;

            Name = name;
        }
    }
}