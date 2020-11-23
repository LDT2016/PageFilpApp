using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CefSharp;
using PageFilpApp.Tools;

namespace PageFilpApp
{
    public partial class Form1 : Form
    {
        #region fields

        private static Form1 instance;
        private ExtChromiumBrowser Browser;
        private readonly string ImagePath = ConfigurationManager.AppSettings["ImagePath"];

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
                if (instance == null)
                {
                    instance = new Form1();
                }

                return instance;
            }
        }

        #endregion

        #region methods

        public void NetInnerSetup()
        {
            try
            {
                var imagePath = $"{Application.StartupPath}\\{ImagePath.TrimStart(@"\".ToCharArray())}";
                var allImages = new DirectoryInfo(imagePath).GetFiles();

                var imageFileName = allImages.Select(x => x.Name)
                                             .ToList()
                                             .OrderBy(x =>
                                                      {
                                                          int.TryParse(Path.GetFileNameWithoutExtension(x), out var intName);

                                                          return intName;
                                                      })
                                             .ToList();

                var imageDataJson = $"var imagesData = [{string.Join(",", imageFileName.Select(x => $"\"./{ImagePath.TrimStart(@"/".ToCharArray()).TrimEnd(@"/".ToCharArray()).Replace("\\", "/")}/{x}\""))}];";
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

                Browser = new ExtChromiumBrowser(regionalNetworkUrl)
                          {
                              Dock = DockStyle.Fill //填充方式
                          };
                Browser.StartNewWindow += Browser_StartNewWindow;
                Browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged; //添加事件
                panel1.Controls.Clear();
                panel1.Controls.Add(Browser);
                panel1.BringToFront();

            }
            catch (Exception) { }
        }


        private void Browser_StartNewWindow(object sender, NewWindowEventArgs e)
        {
            //Browser = new ExtChromiumBrowser(e.url)
            //          {
            //              Dock = DockStyle.Fill //填充方式
            //          };
            //Browser.StartNewWindow += Browser_StartNewWindow;
            //panel1.Controls.Add(Browser);
            //panel1.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetInnerSetup();
        }

        /// <summary>
        /// 开启调试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs args)
        {
            try { }
            catch (Exception) { }
        }

        #endregion
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
