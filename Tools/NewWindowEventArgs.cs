using System;
using CefSharp;

namespace PageFilpApp.Tools
{
    public class NewWindowEventArgs : EventArgs
    {
        #region fields

        private readonly IWindowInfo _windowInfo;

        #endregion

        #region constructors

        public NewWindowEventArgs(IWindowInfo windowInfo, string url)
        {
            _windowInfo = windowInfo;
            this.url = url;
        }

        #endregion

        #region properties

        public string url { get; set; }

        public IWindowInfo WindowInfo
        {
            get => _windowInfo;
            set => value = _windowInfo;
        }

        #endregion
    }
}
