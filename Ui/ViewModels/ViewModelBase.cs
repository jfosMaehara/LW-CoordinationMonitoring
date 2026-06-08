using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ui.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected void NotifyPropertyChanged( [CallerMemberName] string name = "" )
    {
        PropertyChanged?.Invoke( this, new(name) );
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = "")
    {
        if (Equals(field, value)) return false;
        field = value;
        NotifyPropertyChanged(name);

        return true;
    }
}