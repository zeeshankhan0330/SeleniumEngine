using System;
using System.Collections.Generic;
using CoreLibrary.Configuration;
using CoreLibrary.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : IElementFinder
    {
        public IWebElement Find(string location, ElementLocator locator)
        {
            waitForAngular5Load();
            IWebElement element = null;
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(15);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            if (locator == ElementLocator.Id)
            {
                element = fluentWait.Until(x => x.FindElement(By.Id(location)));
            }
            else if (locator == ElementLocator.Css)
            {
                element = fluentWait.Until(x => x.FindElement(By.CssSelector(location)));
            }

            else if (locator == ElementLocator.XPath)
            {
                element = fluentWait.Until(x => x.FindElement(By.XPath(location)));
            }

            else if (locator == ElementLocator.Name)
            {
                element = fluentWait.Until(x => x.FindElement(By.Name(location)));
            }

            else if (locator == ElementLocator.PartialText)
            {
                element = fluentWait.Until(x => x.FindElement(By.PartialLinkText(location)));
            }
            if (element == null)
                throw new NoSuchElementException();
            return element;
        }

        public IList<IWebElement> FindAll(string location, ElementLocator locator)
        {
            waitForAngular5Load();
            IList<IWebElement> element = null; ;
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(15);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            if (locator == ElementLocator.Id)
            {
                element = fluentWait.Until(x => x.FindElements(By.Id(location)));
            }
            else if (locator == ElementLocator.Css)
            {
                element = fluentWait.Until(x => x.FindElements(By.CssSelector(location)));
            }

            else if (locator == ElementLocator.XPath)
            {
                element = fluentWait.Until(x => x.FindElements(By.XPath(location)));
            }

            else if (locator == ElementLocator.Name)
            {
                element = fluentWait.Until(x => x.FindElements(By.Name(location)));
            }

            else if (locator == ElementLocator.PartialText)
            {
                element = fluentWait.Until(x => x.FindElements(By.PartialLinkText(location)));
            }

            return element;
        }

        public bool IsElementPresent(By by)
        {
            throw new NotImplementedException();
        }


        public IWebElement FindNonAngular(string location, ElementLocator locator)
        {
            
            IWebElement element = null;
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(50);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            if (locator == ElementLocator.Id)
            {
                element = fluentWait.Until(x => x.FindElement(By.Id(location)));
            }
            else if (locator == ElementLocator.Css)
            {
                element = fluentWait.Until(x => x.FindElement(By.CssSelector(location)));
            }

            else if (locator == ElementLocator.XPath)
            {
                element = fluentWait.Until(x => x.FindElement(By.XPath(location)));
            }

            else if (locator == ElementLocator.Name)
            {
                element = fluentWait.Until(x => x.FindElement(By.Name(location)));
            }

            else if (locator == ElementLocator.PartialText)
            {
                element = fluentWait.Until(x => x.FindElement(By.PartialLinkText(location)));
            }
            if (element == null)
                throw new NoSuchElementException();
            return element;
        }
    }
}
