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

        public void OnNewWindow(NewWindowEventArgs e)
        {
            StartNewWindow?.Invoke(this, e);
        }
    }
}
