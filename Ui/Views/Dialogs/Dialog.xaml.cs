using System;
using UserControl = System.Windows.Controls.UserControl;

namespace Ui.Views.Dialogs;

public partial class Dialog : UserControl
{
    public Dialog(string message)
    {
        InitializeComponent();
        Message.Text = message;
    }
}