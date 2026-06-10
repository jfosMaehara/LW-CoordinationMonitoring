using Domain.Entities;
using System.Collections.ObjectModel;

namespace Ui.Models;

public class CoordinationMonitoringListModel
{
    /// <summary>
    /// 確認日時
    /// </summary>
    public DateTime _checkDateTime = DateTime.Now;

    /// <summary>
    /// 確認結果
    /// </summary>
    public ObservableCollection<OrderImportEntity> _checkResultEntities = [];
}
