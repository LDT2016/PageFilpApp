using System;
using CefSharp.WinForms;

namespace PageFilpApp.Tools
{
    public class ExtChromiumBrowser : ChromiumWebBrowser
    {
        public ExtChromiumBrowser(string url) : base(url)
        {
            LifeSpanHandler = new CefLifeSpanHandler();
        }

        public event EventHandler<NewWindowEventArgs> StartNewWindow;
        public event EventHandler<ActionEventArgs> NewAction;

        public void OnNewWindow(NewWindowEventArgs e)
        {
            StartNewWindow?.Invoke(this, e);
        }
        public void OnNewAction(ActionEventArgs e)
        {
            NewAction?.Invoke(this, e);
        }
    }
}
