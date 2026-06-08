using Domain.Repositories;
using Infrastructure.ConfigJson;
using Infrastructure.Logtext;
using Infrastructure.Sqlserver;
using Ui.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Ui.ViewModels.Dialogs;

public class DialogProgressViewModel : ViewModelBase
{

    // Models ---------------------------------------------
    public string _message = "";
    public int _value = 0;
    public int _maximum = 0;
    public string _counter = "";

    // Repositories ---------------------------------------


    // Commands -------------------------------------------


    // Properties -----------------------------------------
    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public int Value
    {
        get => _value;
        set
        {
            SetProperty(ref _value, value);
            Counter = Value.ToString() + " / " + Maximum.ToString();
        }
    }

    public int Maximum
    {
        get => _maximum;
        set => SetProperty(ref _maximum, value);
    }

    public string Counter
    {
        get => _counter;
        set => SetProperty(ref _counter, value);
    }



    // Methods --------------------------------------------

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DialogProgressViewModel()
    {
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DialogProgressViewModel(string message = "", int value = 0, int maximum = 0)
    {
        _message = message;
        _value = value;
        _maximum = maximum;
    }

}