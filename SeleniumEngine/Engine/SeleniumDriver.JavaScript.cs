using CoreLibrary.Controls;
using OpenQA.Selenium;
using System;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : IJavaScript
    {
        
      
        public string ExecuteScriptToGetStringData(string script)
        {
            try
            {

                return (string)executor.ExecuteScript("return " + script);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Exception in Method : ExecuteScriptToGetStringData in GenericHelper Class";
            }
        }



        /// <summary>
        /// Executes the script to get bool data.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns></returns>
        public bool ExecuteScriptToGetBoolData(string script)
        {
            
            return (bool)executor.ExecuteScript(script);
        }


        /// <summary>
        /// Executes the script.
        /// </summary>
        /// <param name="script">The script.</param>
        public void ExecuteScript(string script)
        {
            IJavaScriptExecutor executor = ((IJavaScriptExecutor)driver);
            executor.ExecuteScript(script);

        }


        public void ExecuteScript(string script, IWebElement element)
        {
           
            executor.ExecuteScript(script, element);
        }


        public void ExecuteScriptToClickOnAnElement(IWebElement element)
        {
            
            executor.ExecuteScript("arguments[0].click();", element);
        }


        /// <summary>
        /// Scrolls to an element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void ScrollToAnElement(IWebElement element)
        {
            
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public void EnterText(string text, IWebElement element)
        {
            
            executor.ExecuteScript("arguments[0].value=" + text, element);
        }


        public string GetValue(IWebElement element)
        {

           return executor.ExecuteScript("return arguments[0].value", element).ToString();
        }

        public void NonAngularEnterText(string text, IWebElement element)
        {
          
            executor.ExecuteScript("arguments[0].value=" + text, element);
        }
    }
}
