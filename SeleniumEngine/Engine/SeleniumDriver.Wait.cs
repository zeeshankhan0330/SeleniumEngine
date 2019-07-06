using CoreLibrary.Configuration;
using CoreLibrary.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : IWait
    {

        public void WaitUntilVisible(string location, ElementLocator locator)
        {


            if (locator == ElementLocator.Id)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id(location)));
            }
            else if (locator == ElementLocator.Css)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(location)));
            }

            else if (locator == ElementLocator.XPath)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(location)));
            }

            else if (locator == ElementLocator.Name)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name(location)));
            }
        }


        public void WaitForDropdownDataLoad(string element, ElementLocator locator)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(50);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            if (locator == ElementLocator.Id)
            {
                fluentWait.Until(x => x.FindElements(By.Id(element)).Count > 0);
            }
            else if (locator == ElementLocator.Css)
            {
                fluentWait.Until(x => x.FindElements(By.CssSelector(element)).Count > 0);
            }

            else if (locator == ElementLocator.XPath)
            {
                fluentWait.Until(x => x.FindElements(By.XPath(element)).Count > 0);
            }

            else if (locator == ElementLocator.Name)
            {
                fluentWait.Until(x => x.FindElement(By.Name(element)));
            }
        }


        public void waitUntilAngular5Ready()
        {
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    Poll(1000);
                    var angularVersion = executor.ExecuteScript("return getAllAngularRootElements()[0].attributes['ng-version']");
                    if (angularVersion == null)
                    {
                        wait.Until(d => executor.ExecuteScript("return getAllAngularRootElements()[0].attributes['ng-version']") != null);
                    }
                    else if (angularVersion != null)
                    {
                        break;
                    }

                }
                catch (Exception e)
                {
                }
            }
        }


        public void waitForAngular5Load()
        {

             executor.ExecuteAsyncScript(ForAngular, "app-root");
            //bool flag;
            //waitUntilAngular5Ready();

            //bool angularPageLoaded = Boolean.TryParse(executor.ExecuteScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1").ToString(), out flag);
            //if (!angularPageLoaded)
            //{
            //    wait.Until(d => Boolean.TryParse(executor.ExecuteScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1").ToString(), out flag));
            //}
        }




        private void Poll(int milis)
        {
            try
            {
                Thread.Sleep(milis);
            }
            catch (ThreadInterruptedException e)
            {
                Console.Write(e.StackTrace);
            }
        }

        void IWait.WaitForAngular()
        {
            throw new NotImplementedException();
        }

        public const string ForAngular = GetNg1HooksHelper + @"
var rootSelector = arguments[0];
var callback = arguments[1];
if (window.angular && !(window.angular.version && window.angular.version.major > 1)) {
      var hooks = getNg1Hooks(rootSelector);
    if (hooks.$$testability) {
        hooks.$$testability.whenStable(callback);
    } else if (hooks.$injector) {
        hooks.$injector.get('$browser').
        notifyWhenNoOutstandingRequests(callback);
    } else if (!!rootSelector) {
        throw new Error('Could not automatically find injector on page: ""' +
            window.location.toString() + '"". Consider setting rootElement');
    } else {
    throw new Error('root element (' + rootSelector + ') has no injector.' +
        ' this may mean it is not inside ng-app.');
    }
} else if (rootSelector && window.getAngularTestability) {
    var el = document.querySelector(rootSelector);
    window.getAngularTestability(el).whenStable(callback);
} else if (window.getAllAngularTestabilities) {
    var testabilities = window.getAllAngularTestabilities();
    var count = testabilities.length;
    var decrement = function() {
        count--;
        if (count === 0) {
            callback();
        }
    };
    testabilities.forEach(function(testability) {
        testability.whenStable(decrement);
    });
} else if (!window.angular) {
    throw new Error('window.angular is undefined.  This could be either ' +
        'because this is a non-angular page or because your test involves ' +
        'client-side navigation, which can interfere with Protractor\'s ' +
        'bootstrapping.  See http://git.io/v4gXM for details');
} else if (window.angular.version >= 2) {
    throw new Error('You appear to be using angular, but window.' +
        'getAngularTestability was never set.  This may be due to bad ' +
        'obfuscation.');
} else {
    throw new Error('Cannot get testability API for unknown angular ' +
        'version ""' + window.angular.version + '""');
}";



        public const string GetNg1HooksHelper = @"
function getNg1Hooks(selector, injectorPlease) {
    function tryEl(el) {
        try {
            if (!injectorPlease && angular.getTestability) {
                var $$testability = angular.getTestability(el);
                if ($$testability) {
                    return {$$testability: $$testability};
                }
            } else {
                var $injector = angular.element(el).injector();
                if ($injector) {
                    return {$injector: $injector};
                }
            }
        } catch(err) {} 
    }
    function trySelector(selector) {
        var els = document.querySelectorAll(selector);
        for (var i = 0; i < els.length; i++) {
            var elHooks = tryEl(els[i]);
            if (elHooks) {
                return elHooks;
            }
        }
    }
    if (selector) {
        return trySelector(selector);
    } else if (window.__TESTABILITY__NG1_APP_ROOT_INJECTOR__) {
        var $injector = window.__TESTABILITY__NG1_APP_ROOT_INJECTOR__;
        var $$testability = null;
        try {
            $$testability = $injector.get('$$testability');
        } catch (e) {}
        return {$injector: $injector, $$testability: $$testability};
    } else {
        return tryEl(document.body) ||
            trySelector('[ng-app]') || trySelector('[ng\\:app]') ||
            trySelector('[ng-controller]') || trySelector('[ng\\:controller]');
    }
};
";

    }

}

