using System.ComponentModel;

namespace AutoReservation.Common.Extensions
{
    public interface IExtendedNotifyPropertyChanged : INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
}
