using System;
using UserControl = System.Windows.Controls.UserControl;

namespace Ui.Views.Dialogs;

public partial class DialogConfirm : UserControl
{
    public string Message  {get;set;}
    public string Positive {get;set;} = "OK";
    public string Negative {get;set;} = "NG";
    public DialogConfirm(string message)
    {
        Message = message;
        InitializeComponent();
        DataContext = this;
    }
}