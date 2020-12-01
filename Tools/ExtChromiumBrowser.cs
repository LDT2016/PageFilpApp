using System;
using CefSharp.WinForms;

namespace PageFilpApp.Tools
{
    public class ExtChromiumBrowser : ChromiumWebBrowser
    {
        #region constructors

        public ExtChromiumBrowser(string url) : base(url)
        {
            LifeSpanHandler = new CefLifeSpanHandler();
        }

        #endregion

        #region events

        public event EventHandler<ActionEventArgs> NewAction;
        public event EventHandler<NewWindowEventArgs> StartNewWindow;

        #endregion

        #region methods

        public void OnNewAction(ActionEventArgs e)
        {
            NewAction?.Invoke(this, e);
        }

        public void OnNewWindow(NewWindowEventArgs e)
        {
            StartNewWindow?.Invoke(this, e);
        }

        #endregion
    }
}
