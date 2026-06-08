using System;
using System.Windows.Controls;

namespace Ui.Views.Dialogs;
public partial class DialogCircularProgress : UserControl
{
    public DialogCircularProgress(string message)
    {
        InitializeComponent();
        Message.Text = message;
    }
}