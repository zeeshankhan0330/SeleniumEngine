using CoreLibrary.Controls;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : INavigate
    {
        /// <summary>
        /// Navigates to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void NavigateToUrl(string url)
        {

            driver.Navigate().GoToUrl(url);
        }


        public void RefreshPage()
        {

            driver.Navigate().Refresh();

        }


        public string GetURL()
        {
            waitForAngular5Load();
            return driver.Url;
        }

        public string GetTitle()
        {
            return driver.Title;
        }


        public void CloseQuitAndDispose()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }

     
    }
}
