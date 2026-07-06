using HoangTramHuynh.Component;
using HoangTramHuynh.Contexts;
using HoangTramHuynh.DataObject;
using HoangTramHuynh.Page;
using NUnit.Framework;
using Reqnroll;
using Reqnroll.Assist;

namespace HoangTramHuynh.Contexts
{
    public class StudentRegistrationContext
    {
        public RegisterData RegisterData { get; set; } = new RegisterData();
    }
}