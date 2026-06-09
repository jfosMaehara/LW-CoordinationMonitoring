using System.Windows;
using Ui.ViewModels;

namespace Ui.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private NotifyIcon _notifyIcon;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void SetupTrayIcon()
    {

    }
}