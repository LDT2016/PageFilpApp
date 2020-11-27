using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PageFilpApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var appEvents = new ApplicationEventHandlerClass();
            Application.ThreadException += appEvents.OnThreadException; Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();
            Application.Run(Form1.Instance);
        }

        #region nested types

        // 全局异常处理
        public class ApplicationEventHandlerClass
        {
            #region methods

            public void OnThreadException(object sender, ThreadExceptionEventArgs e)
            {
                MessageBox.Show(e.Exception.Message);
            }

            #endregion
        }

        #endregion
    }
}
