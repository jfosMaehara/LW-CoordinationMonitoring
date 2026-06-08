
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Ui.Behaviors;


/// <summary>
/// カレンダーを月表示のみにするビヘイビア
/// </summary>
[TypeConstraint(typeof(Calendar))]
public class CalenderMonthModeBefavior : Behavior<Calendar>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.Loaded += (s, e) =>
        {
            // マテリアルデザインの初期月表示カルチャインフォがうまく適応されないバグ対応。
            AssociatedObject.DisplayMode = CalendarMode.Year;

            // 月しか選ばせないので、1日が表示されるようにする。
            AssociatedObject.DisplayDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-01"));
        };

        AssociatedObject.DisplayModeChanged += (s, e) =>
        {
            AssociatedObject.DisplayMode = CalendarMode.Year;
            Mouse.Capture(null);  // これをしないと選択月からフォーカスが常に動いてしまう。
        };
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.DisplayModeChanged -= (s, e) => {};
    }
}