using System;

namespace PageFilpApp.Tools
{
    public class ActionEventArgs : EventArgs
    {
        #region fields

        #endregion

        #region constructors

        public ActionEventArgs(int command)
        {
            Command = command;
        }

        #endregion

        #region properties

        public int Command { get; set; }

        #endregion
    }
}
