using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HuynhHoangTram.Core;

namespace HuynhHoangTram.Component
{
    public class FileUpload
    {
        private readonly WebObject _fileInput;

        public FileUpload(WebObject fileInput)
        {
            _fileInput = fileInput;
        }

        public void SelectFile(string filePath)
        {
            ClickChooseFile();
            ChooseFileFromFolder(filePath);
            EnsureFileSelected();
        }

        public void ClickChooseFile()
        {
            _fileInput.WaitForElementToBeVisible();
        }

        public void ChooseFileFromFolder(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be empty.");
            }
            string absolutePath = Path.GetFullPath(filePath);
            if (!File.Exists(absolutePath))
            {
                throw new FileNotFoundException("File does not exist.", absolutePath);
            }
            IWebElement element = _fileInput.WaitForElementToBeVisible();
            element.SendKeys(absolutePath);
        }

        public bool IsFileSelected()
        {
            string value = _fileInput.GetAttributeValue("value");
            return !string.IsNullOrWhiteSpace(value);
        }

        public void EnsureFileSelected()
        {
            if (!IsFileSelected())
            {
                throw new Exception("File was not selected successfully.");
            }
        }
    }
}