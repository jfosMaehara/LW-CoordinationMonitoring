using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Ui.Views;

namespace Ui.Tray;

public class TrayApplicationContext : ApplicationContext
{
    private NotifyIcon notifyIcon = new NotifyIcon();

    public TrayApplicationContext()
    {
        try
        {
            // コンテキストメニュー作成
            var menu = new ContextMenuStrip();
            menu.Items.Add("開く", null, OnOpenClicked);
            menu.Items.Add("終了", null, OnExitClicked);

            //Uri iconUri = new Uri("pack://application:,,,/Resources/zaiko.ico", UriKind.Absolute);
            //Icon icon = new Icon(iconUri);
            var asm = Assembly.GetExecutingAssembly();
            using var stream = asm.GetManifestResourceStream("Ui.Resources.zaiko.ico");
            Icon icon = new Icon(stream!);
            // NotifyIcon作成
            notifyIcon = new NotifyIcon
            {
                Icon = icon, //任意のアイコンに変更可
                Text = "連携モニタリング",
                Visible = true,
                ContextMenuStrip = menu
            };

            // ダブルクリックで開く
            notifyIcon.DoubleClick += OnOpenClicked;
        }
        catch (Exception ex)
        {
            MessageBox.Show("初期化エラー:" + ex.Message);
            ExitThread();
        }
    }

    private void OnOpenClicked(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            var window = new Views.MainWindow();
            window.Show();

        }
        catch (Exception ex)
        {

        }
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        notifyIcon.Visible = false;
        notifyIcon.Dispose();
        ExitThread();
    }
}
