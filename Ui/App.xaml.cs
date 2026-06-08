using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using Domain.Repositories;
using Domain.StaticValues;
using Infrastructure.ConfigJson;
using Infrastructure.Logtext;


namespace Ui;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// APPコンフィグのレポジトリ AppConfig.Jsonからのコンフィグ値を読み込み、書き込みをする。
    /// </summary>
    public IAppConfigRepository _appConfigRepository = new AppConfigRepositoryImpl();

    /// <summary>
    /// DBコンフィグのレポジトリ DbConfig.Jsonからのコンフィグ値を読み込み、書き込みをする。
    /// </summary>
    public IDbConfigRepository _dbConfigRepository = new DbConfigRepositoryImpl();

    /// <summary>
    /// Loggerのレポジトリ パスは AppConfigからの取り出し。 
    /// </summary>
    public static ILoggerRepository Logger { get; set;} = new LoggerRepositoryImpl(AppConfig.LogPath);

    /// <summary>
    /// Mutex処理用
    /// </summary>
    public Mutex? Mutex { get; set; }

    public App()
    {
        // Appコンフィグ
        _appConfigRepository.GetConfig();
        _appConfigRepository.SaveStaticValue();

        // Dbコンフィグ
        _dbConfigRepository.GetConfig();
        _dbConfigRepository.SaveStaticValue();

        // Logger
        Logger = new LoggerRepositoryImpl(AppConfig.LogPath);
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Mutex = new Mutex(false, AppConfig.ApplicationTitle );
        if (!Mutex.WaitOne(0, false))
        {
            Mutex.Close();
            Shutdown();
        }
        else
        {
            //Applicatioin起動処理//
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            var window = new Views.MainWindow();
            window.Show();
        }
    }

    private void App_Exit(object sender, ExitEventArgs e)
    {
        if (Mutex != null )
        {
            Mutex.ReleaseMutex();
            Mutex.Close();
        }
    }
}
