using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CefSharp;
using PageFilpApp.Tools;
using PageFilpApp.Util;

namespace PageFilpApp
{
    public partial class Form1 : Form
    {
        #region delegates

        public delegate void BrowserActionDelegate();

        #endregion

        #region fields

        private static Form1 _instance;
        private readonly string _imagePath = ConfigurationManager.AppSettings["ImagePath"];
        private ExtChromiumBrowser _browser;

        #endregion

        #region constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region properties

        public static Form1 Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Form1();
                    _instance.NetInnerSetup();
                }

                return _instance;
            }
        }

        #endregion

        #region methods

        public void NetInnerSetup()
        {
            try
            {
                var imagePath = $"{Application.StartupPath}\\{_imagePath.TrimStart(@"\".ToCharArray())}";
                var allImages = new DirectoryInfo(imagePath).GetFiles();

                var imageFileName = allImages.Select(x => x.Name)
                                             .ToList()
                                             .OrderBy(x =>
                                                      {
                                                          int.TryParse(Path.GetFileNameWithoutExtension(x), out var intName);

                                                          return intName;
                                                      })
                                             .ToList();

                var imageDataJson = $"var imagesData = [{string.Join(",", imageFileName.Select(x => $"\"./{_imagePath.TrimStart(@"/".ToCharArray()).TrimEnd(@"/".ToCharArray()).Replace("\\", "/")}/{x}\""))}];";
                var imageJsPath = Application.StartupPath + "\\images.js";

                if (File.Exists(imageJsPath))
                {
                    File.Delete(imageJsPath);
                }

                var sw = new StreamWriter(imageJsPath, true);
                sw.WriteLine(imageDataJson);
                sw.Flush();
                sw.Close();

                var regionalNetworkUrl = Application.StartupPath + "\\index.html";

                _browser = new ExtChromiumBrowser(regionalNetworkUrl)
                           {
                               Dock = DockStyle.Fill //填充方式
                           };

                //_browser.StartNewWindow += Browser_StartNewWindow;
                //_browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged; //添加事件
                _browser.NewAction += Browser_NewAction;
                _browser.MenuHandler = new MenuHandler();
                panel1.Controls.Clear();
                panel1.Controls.Add(_browser);
            }
            catch (Exception ex)
            {
                Logs.Save(ex);
            }
        }

        private void Browser_NewAction(object sender, ActionEventArgs e)
        {
            if (e.Command == 1)
            {
                if (InvokeRequired)
                {
                    var action = new BrowserActionDelegate(() => { _browser.Reload(); });
                    Invoke(action);
                }
                else
                {
                    _browser.Reload();
                }
            }
        }

        #endregion

        //private void Browser_StartNewWindow(object sender, NewWindowEventArgs e)
        //{
        //    //Browser = new ExtChromiumBrowser(e.url)
        //    //          {
        //    //              Dock = DockStyle.Fill //填充方式
        //    //          };
        //    //Browser.StartNewWindow += Browser_StartNewWindow;
        //    //panel1.Controls.Add(Browser);
        //    //panel1.BringToFront();
        //}

        ///// <summary>
        ///// 开启调试
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //private void OnIsBrowserInitializedChanged(object sender, EventArgs args)
        //{
        //    try { }
        //    catch (Exception ex)
        //    {
        //        Logs.Save(ex);
        //    }
        //}
    }
}
