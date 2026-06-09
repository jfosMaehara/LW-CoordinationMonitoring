using System;
using UserControl = System.Windows.Controls.UserControl;
using Ui.ViewModels;
using Ui.ViewModels.Dialogs;

namespace Ui.Views.Dialogs;

public partial class DialogProgress : UserControl
{
    public DialogProgress()
    {
        InitializeComponent();
    }

    public DialogProgress(DialogProgressViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

}