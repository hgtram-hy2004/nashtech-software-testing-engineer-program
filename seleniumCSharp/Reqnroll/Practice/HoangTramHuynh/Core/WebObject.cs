using System;
using OpenQA.Selenium;
namespace HoangTramHuynh.Core;

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
