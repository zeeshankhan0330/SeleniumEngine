using CoreLibrary.Configuration;
using CoreLibrary.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : IDropDown
    {

        /// <summary>
        /// Selects the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="index">The index.</param>
        public void SelectElement(IWebElement element, int index)
        {
            
            SelectElement select = new SelectElement(element);
            select.SelectByIndex(index);

        }


        /// <summary>
        /// Selects the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public void SelectElement(IWebElement element, string value, DropDownValue attributeType)
        {
            SelectElement select = new SelectElement(element);
            if (attributeType == DropDownValue.Value)
                select.SelectByValue(value);
            else if (attributeType == DropDownValue.Name)
                select.SelectByText(value);
        }






        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public string GetAllItems(IWebElement element)
        {

            SelectElement select = new SelectElement(element);
            List<IWebElement> list = select.Options.ToList();
            List<string> newList = new List<string>();
            string listOfItems = "";
            foreach (IWebElement ele in list)
            {
                newList.Add(ele.Text);
                listOfItems = listOfItems + " " + ele.Text;
            }

            return listOfItems;

        }

        /// <summary>
        /// Gets the selected value from the dropdown.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public string GetSelectedValueFromTheDropdown(IWebElement element)
        {

            SelectElement select = new SelectElement(element);
            string selectedValue = select.SelectedOption.Text;
            return selectedValue;
        }
    }
}
