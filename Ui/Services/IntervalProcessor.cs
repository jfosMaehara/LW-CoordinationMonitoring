using System;
using System.Timers;
using Ui.ViewModels;

namespace Ui.Services;

public class IntervalProcessor
{
    private readonly System.Timers.Timer _timer;
    private int _count = 0;

    // イベントで結果を通知
    public event Action<CoordinationMonitoringListViewModel> OnResultGenerated;

    public IntervalProcessor(double intervalMilliseconds)
    {
        if (intervalMilliseconds <= 0) throw new ArgumentException("Interval must be greater than zero.");

        _timer = new System.Timers.Timer(intervalMilliseconds);
        _timer.Elapsed += TimerElapsed;
        _timer.AutoReset = true;    // 繰り返し実行
        //_timer.Enabled = false; //すぐに実行しない

        // 初回は即時実行
        //TimerElapsed(null, null);
    }

    public void Start() => _timer.Start();
    public void Stop() => _timer.Stop();

    private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        _count++;

        // 結果クラスを作成
        var result = new CoordinationMonitoringListViewModel();
        result.CheckingOrderImportListCommand.Execute(null);
        // イベントで返す
        OnResultGenerated?.Invoke(result);
    }
}
