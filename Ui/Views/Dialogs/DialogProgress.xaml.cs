using System;
using System.Windows.Controls;
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