using System;
using CefSharp;

namespace PageFilpApp.Tools
{
    public class CefLifeSpanHandler : ILifeSpanHandler
    {
        #region constructors

        #endregion

        #region methods

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }

            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser) { }
        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser) { }

        public bool OnBeforePopup(IWebBrowser browserControl,
                                  IBrowser browser,
                                  IFrame frame,
                                  string targetUrl,
                                  string targetFrameName,
                                  WindowOpenDisposition targetDisposition,
                                  bool userGesture,
                                  IPopupFeatures popupFeatures,
                                  IWindowInfo windowInfo,
                                  IBrowserSettings browserSettings,
                                  ref bool noJavascriptAccess,
                                  out IWebBrowser newBrowser)
        {
            var chromiumWebBrowser = (ExtChromiumBrowser)browserControl;

            chromiumWebBrowser.Invoke(new Action(() =>
                                                 {
                                                     var e = new NewWindowEventArgs(windowInfo, targetUrl);
                                                     chromiumWebBrowser.OnNewWindow(e);
                                                 }));

            newBrowser = null;

            return true;
        }

        #endregion
    }
}
