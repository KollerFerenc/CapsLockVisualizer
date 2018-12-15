using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CapsLockVisualizer
{
    public partial class Form1 : Form
    {
        NotifyIcon CapsLockIcon;
        Icon activeIcon;
        Icon idleIcon;  
        Thread capslockWorker;


        public Form1()
        {
            InitializeComponent();

            activeIcon = new Icon("on.ico");
            idleIcon = new Icon("off.ico");
            CapsLockIcon = new NotifyIcon();
            CapsLockIcon.Icon = idleIcon;
            CapsLockIcon.Visible = true;

            MenuItem quitMenuItem = new MenuItem("Quit");
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(quitMenuItem);
            CapsLockIcon.ContextMenu = contextMenu;

            quitMenuItem.Click += QuitMenuItem_Click;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            capslockWorker = new Thread(new ThreadStart(CapsLockThread));
            capslockWorker.Start();
        }

        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            CapsLockIcon.Dispose();
            this.Close();
        }

        public void CapsLockThread()
        {
            try
            {
                bool isCapsLocked = Control.IsKeyLocked(Keys.CapsLock);
                while (true)
                {
                    isCapsLocked = Control.IsKeyLocked(Keys.CapsLock);
                    if (isCapsLocked)
                    {
                        CapsLockIcon.Icon = activeIcon;
                    }
                    else
                    {
                        CapsLockIcon.Icon = idleIcon;
                    }
                    Thread.Sleep(100);
                }
            }
            catch(ThreadAbortException tae)
            {

            }
            catch
            {

            }
        }
    }
}
