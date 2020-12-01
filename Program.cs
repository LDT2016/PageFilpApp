using System;
using System.Threading;
using System.Windows.Forms;

namespace PageFilpApp
{
    static class Program
    {
        #region methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var appEvents = new ApplicationEventHandlerClass();
            Application.ThreadException += appEvents.OnThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();
            Application.Run(Form1.Instance);
        }

        #endregion

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
