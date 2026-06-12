using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ui.Models;

namespace Ui.ViewModels;

public class CoordinationMonitoringListViewModel : ViewModelBase
{
    public CoordinationMonitoringListModel _model = new();

    /// <summary>
    /// 確認日時
    /// </summary>
    public DateTime CheckDateTime
    {
        get => _model._checkDateTime;
        set => SetProperty(ref _model._checkDateTime, value);
    }

    /// <summary>
    /// 確認結果
    /// </summary>
    public ObservableCollection<OrderImportEntity> CheckResultEntities
    {
        get => _model._checkResultEntities;
        set => SetProperty(ref _model._checkResultEntities, value);
    }

    public Commands.CheckingOrderImportListCommand _checkingOrderImportListCommand;
    public ICommand CheckingOrderImportListCommand => _checkingOrderImportListCommand;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CoordinationMonitoringListViewModel()
    {
        _checkingOrderImportListCommand = new(this);
        //CheckingOrderImportListCommand.Execute(null);
    }
}
