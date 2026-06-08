using Domain.Repositories;
using Domain.StaticValues;
using Infrastructure.ConfigJson;
using Infrastructure.Logtext;
using Infrastructure.Sqlserver;
using Ui.Models;
using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Windows.Input;
using System.Data;
using System.Windows.Controls;
using Ui.Views;
using Microsoft.Identity.Client;

namespace Ui.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    // Models ---------------------------------------------
    public MainWindowModel _model = new();

    // Repositories ---------------------------------------

    // Commands -------------------------------------------


    // Properties -----------------------------------------

    /// <summary>
    /// アプリケーション自体の表示タイトル。アプリケーション枠として表示されるタイトル。
    /// </summary>
    public static string ApplicationTitle => AppConfig.ApplicationTitle;

    /// <summary>
    /// アプリケーションの表示名。カラーゾーン上に表示されるタイトル。
    /// </summary>
    public static string ApplicationName => AppConfig.ApplicationName;

    /// <summary>
    /// version表示用 getonly
    /// </summary>
    public static string Version => "Ver " + ApplicationStaticValue.ApplicationVersion;

    /// <summary>
    /// デイトピッカー 日本語用カルチャインフォ
    /// </summary>
    public static System.Globalization.CultureInfo JpCulture { get; } = new("ja-JP");

    /// <summary>
    /// デイトピッカー 日本語用カルチャインフォ XAMLにバインドするためのプロパティ
    /// </summary>
    public System.Windows.Markup.XmlLanguage DatePickerLanguage { get; }


    // ViewContent ----------------------------------------
    //public UserControl Content { get; set; } = new BeforeLocationSelectView();


    // Methods --------------------------------------------

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public MainWindowViewModel()
    {
        // デイトピッカー日本語対応用
        DatePickerLanguage = System.Windows.Markup.XmlLanguage.GetLanguage(JpCulture.Name);

        App.Logger.LogWrite(ILoggerRepository.LogLevel.Debug, "test_log");

        // 初期表示 Viewコンテンツとデータコンテキスト
        /*
        Content = new BeforeLocationSelectView
        {
            DataContext = new BeforeLocationSelectViewModel()
        };
        */
    }

}