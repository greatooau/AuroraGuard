using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AuroraGuard.Core.Abstract;

public class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// It will notify that the <see cref="propertyName"/> value has changed
    /// </summary>
    /// <param name="propertyName">The name of the property from which will be notified to the UI</param>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// It compares if the value and the field have the same value. And if so, it calls <see cref="OnPropertyChanged"/> to
    /// notify the UI
    /// </summary>
    /// <typeparam name="T">The type of the field and value to be assigned</typeparam>
    /// <param name="field">The field to assign the value</param>
    /// <param name="value">The value to be assigned</param>
    /// <param name="propertyName">The name of the property from which will be notified to the UI</param>
    /// <returns><see langword="true"/> if the value contained in the field is NOT equal to the <see cref="value"/> provided</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        
        field = value;
        OnPropertyChanged(propertyName);

        return true;
    }
}