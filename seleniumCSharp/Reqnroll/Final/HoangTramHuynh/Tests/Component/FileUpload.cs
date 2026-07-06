using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;

namespace HoangTramHuynh.Component
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
            ReportLog.Info("Clicking on 'Choose File' button.");
        }

        public void ChooseFileFromFolder(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be empty.");
            }

            string absolutePath;

            if (Path.IsPathRooted(filePath))
            {
                absolutePath = filePath;
            }
            else
            {
                string projectRoot = Path.GetFullPath(
                    Path.Combine(AppContext.BaseDirectory, "..", "..", "..")
                );

                absolutePath = Path.Combine(projectRoot, filePath);
            }

            if (!File.Exists(absolutePath))
            {
                throw new FileNotFoundException(
                    "File does not exist: " + absolutePath,
                    absolutePath
                );
            }

            IWebElement element = _fileInput.WaitForElementToBeVisible();
            element.SendKeys(absolutePath);
            ReportLog.Info($"Selected file '{absolutePath}' for upload.");
        }

        public bool IsFileSelected()
        {
            string value = _fileInput.GetAttributeValue("value");
            ReportLog.Info($"File input value after selection: '{value}'");
            return !string.IsNullOrWhiteSpace(value);
        }

        public void EnsureFileSelected()
        {
            if (!IsFileSelected())
            {
                throw new Exception("File was not selected successfully.");
            }
            ReportLog.Info("File selection is confirmed.");
        }
    }
}