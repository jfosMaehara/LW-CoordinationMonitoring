using Domain.Repositories;
using Domain.StaticValues;
using Infrastructure.ConfigJson;
using Infrastructure.Logtext;
using Infrastructure.Sqlserver.RepositoriesImpl;
using Infrastructure.Access.RepositoriesImpl;
using System.Windows.Input;
using Ui.ViewModels;

namespace Ui.Commands;

public class CheckingOrderImportListCommand(CoordinationMonitoringListViewModel vm) : ICommand
{
    public CoordinationMonitoringListViewModel _vm = vm;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        var repo = new OrderImportEntityListRepositoryImpl();
        var list = repo.GetEntityList();
        var mdbRepo = new CoordinationMDBCheckRepositoryImpl(list);
        var mdbList = mdbRepo.GetEntityList();
        _vm.CheckDateTime = DateTime.Now;
        _vm.CheckResultEntities = [.. mdbList];
    }
}
