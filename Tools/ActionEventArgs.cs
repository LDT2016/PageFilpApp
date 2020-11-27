using System;
using CefSharp;

namespace PageFilpApp.Tools
{
    public class ActionEventArgs : EventArgs
    {
        private int _command;
        public ActionEventArgs(int command)
        {
            _command = command;
        }

        public int Command
        {
            get => _command;
            set => _command = value;
        }
    }
}
