using System.ComponentModel;
using System.Runtime.Serialization;
using AutoReservation.Common.Extensions;

namespace AutoReservation.Common.DataTransferObjects.Core
{
    [DataContract]
    public abstract class DtoBase<T> : IExtendedNotifyPropertyChanged, ICloneable<T>, IValidatable
    {
        public abstract string Validate();
        public abstract T Clone();

        public event PropertyChangedEventHandler PropertyChanged;

        void IExtendedNotifyPropertyChanged.OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
