using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace tickMeter.Forms
{
    
    public partial class TickrateStatistics : Form
    {
        public class CustomMenuHandler : IContextMenuHandler
        {

            public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
            {
                model.Clear();
                return;
            }

            public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
            {
                return false;
            }

            public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
            {
                return;
            }

            public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
            {
                return false;
            }
        }

        private readonly ChromiumWebBrowser browser;
        public TickrateStatistics()
        {
            InitializeComponent();
            
            browser = new ChromiumWebBrowser("https://it-man.website/tickmeter/stats/?partial=1&current_week=1")
            {
                Dock = DockStyle.Fill,
                MenuHandler = new CustomMenuHandler(),
            };

            Controls.Add(browser);
        }

        private void TickrateStatistics_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
