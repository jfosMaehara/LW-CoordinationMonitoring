using System;
using UserControl = System.Windows.Controls.UserControl;

namespace Ui.Views.Dialogs;
public partial class DialogCircularProgress : UserControl
{
    public DialogCircularProgress(string message)
    {
        InitializeComponent();
        Message.Text = message;
    }
}