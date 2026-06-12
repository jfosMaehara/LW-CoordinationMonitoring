using Domain.StaticValues;
using Infrastructure.Logtext;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Toolkit.Uwp.Notifications.Internal;
using System;
using System.Drawing;       // Icon
using System.Reflection;
using System.Windows;
using System.Windows.Forms; // NotifyIcon
using Ui.Services;
using Ui.ViewModels;
using Windows.ApplicationModel.Background;

namespace Ui.Views;

/// <summary>
/// CoordinationMonitoringListView.xaml の相互作用ロジック
/// </summary>
public partial class CoordinationMonitoringListView : Window
{
    private LoggerRepositoryImpl _logger = new LoggerRepositoryImpl(AppConfig.LogPath);
    private static NotifyIcon _notifyIcon;
    private bool _isExit;
    private bool _isFirstMinimize = true; //初回最小化判定
    private IntervalProcessor processor;

    public CoordinationMonitoringListView()
    {
        InitializeComponent();
        DataContext = new CoordinationMonitoringListViewModel();
        InitTrayIcon();
        // Closing イベント登録
        this.Closing += Window_Closing;
        //this.Loaded += Window_Loaded;
        this.StateChanged += Window_StateChanged;
        // 通知（のボタンをクリック）
        ToastNotificationManagerCompat.OnActivated += toastArgs =>
        {
            ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                ShowMainWindow();
            });
        };

        processor = new IntervalProcessor(AppConfig.ConfirmationIntervalMillisecond); //５分
        // 結果を受け取る
        processor.OnResultGenerated += TimerChecked;
        processor.Start();
    }

    private void InitTrayIcon()
    {
        var asm = Assembly.GetExecutingAssembly();
        using var stream = asm.GetManifestResourceStream("Ui.Resources.zaiko.ico");
        Icon icon = new Icon(stream!);
        _notifyIcon = new NotifyIcon
        {
            Icon = icon,
            Text = "連携モニタリング",
            Visible = true
        };

        // コンテキストメニュー
        var menu = new ContextMenuStrip();
        menu.Items.Add("モニタリング画面", null, (s, e) => ShowMainWindow());
        menu.Items.Add("終了", null, (s, e) => ExitApplication());
        _notifyIcon.ContextMenuStrip = menu;

        // ダブルクリックで表示
        _notifyIcon.DoubleClick += (s, e) => ShowMainWindow();
        // バルーンクリックで表示
        _notifyIcon.BalloonTipClicked += (s,e) => ShowMainWindow();
    }

    private void TimerChecked(CoordinationMonitoringListViewModel result)
    {
        Dispatcher.Invoke(() =>
        {
            var viewModel = (CoordinationMonitoringListViewModel)DataContext;
            viewModel.CheckDateTime = DateTime.Now;
            viewModel.CheckResultEntities.Clear();
            if (result.CheckResultEntities.Count > 0)
            {
                viewModel.CheckResultEntities = result.CheckResultEntities;
                ShowNotification();
            }
        });
    }

    /// <summary>
    /// ウインドウを再表示
    /// </summary>
    private void ShowMainWindow()
    {
        Show();
        WindowState = WindowState.Normal;
        Activate();
    }

    /// <summary>
    /// アプリ終了
    /// </summary>
    private void ExitApplication()
    {
        _isExit = true;
        processor.Stop();
        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        System.Windows.Application.Current.Shutdown();
    }

    private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }

    /// <summary>
    /// 最小化時にトレイに格納
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_StateChanged(object? sender, EventArgs e)
    {
        if (WindowState == WindowState.Minimized)
        {
            Hide();
        }
    }

    //private void Window_Loaded(object sender, RoutedEventArgs e)
    //{
    //    Hide();
    //    //ShowBalloonTip("アプリはバックグラウンドで起動しました");
    //}

    /// <summary>
    /// バルーン通知表示
    /// </summary>
    /// <param name="message"></param>
    private void ShowBalloonTip(string message)
    {

        _notifyIcon.BalloonTipTitle = "連携モニタリング";
        _notifyIcon.BalloonTipText = message;
        _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
        _notifyIcon.ShowBalloonTip(30000); // 30秒表示
    }

    private void ShowNotification()
    {
        new ToastContentBuilder()
            .AddHeader("coordinationmonitoring", "連携モニタリング", "action")
            .AddText("未連携があります")
            //.AddButton(new ToastButton()
            //                .SetContent("開く")
            //                .AddArgument("action", "open")
            //                )
            .AddButton("モニタリング画面", ToastActivationType.Foreground, "open")
            .Show();
    }
}
