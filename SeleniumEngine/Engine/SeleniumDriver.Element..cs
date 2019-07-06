using CoreLibrary.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections;
using System.Collections.Generic;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : IElement
    {
        public void Click(IWebElement element)
        {
            waitForAngular5Load();
            element.Click();
        }

        public void NonAngularClick(IWebElement element)
        {
            element.Click();
        }


        public void ClearText(IWebElement element)
        {
            waitForAngular5Load();
            element.Clear();

        }

        public void NonAngularClearText(IWebElement element)
        {

                 element.Clear();

        }


        public string GetText(IWebElement element)
        {
            waitForAngular5Load();
            return element.Text;
        }

        public string innerHtml(IWebElement element)
        {
            waitForAngular5Load();
            return element.GetAttribute("innerHTML");
        }

        public string outerHtml(IWebElement element)
        {
            waitForAngular5Load();
            return element.GetAttribute("outerHTML");
        }

        public string className(IWebElement element)
        {
            waitForAngular5Load();
            return element.GetAttribute("class");
        }

        public void EnterText(IWebElement element, string value)
        {
            waitForAngular5Load();
            element.Clear();
            element.SendKeys(value);

        }


        public void NonAngularEnterText(IWebElement element, string value)
        {
            NonAngularClearText(element);
            element.SendKeys(value);

        }


        public void MouseClick(IWebElement element)
        {
            waitForAngular5Load();
            Actions _act = new Actions(driver);
            _act.MoveToElement(element).Click().Build().Perform();
        }

        public IList<IWebElement> Child(IWebElement element)
        {
            IList<IWebElement> child = element.FindElements(By.XPath("./*"));
            return child;
        }

        public IList<IWebElement> Parent(IWebElement element)
        {
            IList<IWebElement> Parent = element.FindElements(By.XPath("./.."));
            return Parent;
        }

        public IList<IWebElement> Sibling(IWebElement element)
        {
            IList<IWebElement> Sibling = element.FindElements(By.XPath("following-sibling::*"));
            return Sibling;
        }
    }
}
