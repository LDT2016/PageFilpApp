using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace PageFilpApp.Util
{
    public class Logs
    {
        #region methods

        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            var AppPath = AppDomain.CurrentDomain.BaseDirectory;

            if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled)
                     .Success)
            {
                AppPath = AppPath.Substring(0, AppPath.Length - 1);
            }

            return AppPath;
        }

        public static void Save(int msg)
        {
            Save(msg.ToString());
        }

        public static void Save(Exception ex)
        {
            Save("Exception:"
                 + "\r\n\r\nException Message: \r\n"
                 + ex.Message
                 + "\r\n"
                 + "\r\nBase Exception: \r\n"
                 + ex.GetBaseException()
                 + "\r\n"
                 + "\r\nStack Trace: \r\n"
                 + ex.StackTrace
                 + "\r\n"
                 + (ex.InnerException != null
                        ? "\r\nInner Exception: \r\n" + ex.InnerException.Message
                        : string.Empty));
        }

        public static void Save(string msg)
        {
            var logpath = GetRootPath() + "\\ExecuteLog.log";
            var sw = new StreamWriter(logpath, true);
            sw.WriteLine("********************************************************");
            sw.Write(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            sw.Write("    Server: ");

            sw.WriteLine(string.IsNullOrEmpty(Environment.MachineName)
                             ? string.Empty
                             : Environment.MachineName);
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }

        #endregion
    }
}
