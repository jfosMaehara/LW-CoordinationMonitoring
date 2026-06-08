using System;
using System.Windows.Controls;

namespace Ui.Views.Dialogs;

public partial class Dialog : UserControl
{
    public Dialog(string message)
    {
        InitializeComponent();
        Message.Text = message;
    }
}