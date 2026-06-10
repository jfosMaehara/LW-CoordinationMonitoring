using System.Windows;
using Ui.ViewModels;

namespace Ui.Views;

/// <summary>
/// CoordinationMonitoringListView.xaml の相互作用ロジック
/// </summary>
public partial class CoordinationMonitoringListView : Window
{
    public CoordinationMonitoringListView()
    {
        InitializeComponent();
        DataContext = new CoordinationMonitoringListViewModel();
    }
}
