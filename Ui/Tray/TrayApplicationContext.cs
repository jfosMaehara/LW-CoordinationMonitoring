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
using Ui.ViewModels;
using System;
using System.Threading;
using Domain.StaticValues;
using System.Windows.Threading;

namespace Ui.Tray;

public class TrayApplicationContext : ApplicationContext
{
    private static NotifyIcon notifyIcon = new NotifyIcon();
    //public static Views.CoordinationMonitoringListView window = new Views.CoordinationMonitoringListView();
    // タイマーを保持するフィールド（GCで消えないようにする）
    private static System.Threading.Timer _timer;

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

            // インターバル
            int oneHour = AppConfig.ConfirmationIntervalMillisecond;
            _timer = new System.Threading.Timer(DoWork, null, 0, oneHour);
            
        }
        catch (Exception ex)
        {
            MessageBox.Show("初期化エラー:" + ex.Message);
            ExitThread();
        }
    }

    private static void DoWork(object state)
    {
        try
        {
            var window = new CoordinationMonitoringListView();
            System.Windows.Threading.Dispatcher.Run();
            //RunTask(state);
        }
        catch { }
    }

    private static void RunTask(object state)
    {
        //var window = (CoordinationMonitoringListView)state;
        //var viewModel = (CoordinationMonitoringListViewModel)window.DataContext;
        //viewModel.CheckingOrderImportListCommand.Execute(null);

        //var viewModel = new CoordinationMonitoringListViewModel();
        //viewModel.CheckingOrderImportListCommand.Execute(null);
        //Dispatcher.CurrentDispatcher.Invoke(() =>
        //{
        //    window.DataContext = viewModel;
        //});
        var window = new CoordinationMonitoringListView();
        var viewModel = (CoordinationMonitoringListViewModel)window.DataContext;
        if (viewModel.CheckResultEntities.Count  > 0)
        {
            notifyIcon.BalloonTipTitle = "連携モニタリング";
            notifyIcon.BalloonTipText = "未連携があります。";
            notifyIcon.ShowBalloonTip(3000);
        }
    }

    private static void OutputToast()
    {
        try
        {
            string title = "連携モニタリング";
            string message = "未連携があります。";
            
        }
        catch { }
    }

    private void OnOpenClicked(object sender, EventArgs e)
    {
        try
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            //var window = new Views.MainWindow();
            var window = new Views.CoordinationMonitoringListView();
            window.ShowDialog();
            //var viewModel = (MainWindowViewModel)window.DataContext;
            //var isOpenAft = System.Windows.Application.Current.Windows.OfType<MainWindow>().Any();
            //notifyIcon.ContextMenuStrip!.Items[0].Enabled = false;
            
        }
        catch (Exception ex)
        {

        }
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        _timer.Dispose();
        notifyIcon.Visible = false;
        notifyIcon.Dispose();
        ExitThread();
    }
}
