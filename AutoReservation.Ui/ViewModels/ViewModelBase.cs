using System.Collections.Generic;
using AutoReservation.Common.Interfaces;
using AutoReservation.Ui.Factory;
using System.ComponentModel;
using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;
using AutoReservation.Common.Extensions;

namespace AutoReservation.Ui.ViewModels
{
    public abstract class ViewModelBase : IExtendedNotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IServiceFactory factory;

        protected ViewModelBase(IServiceFactory factory)
        {
            this.factory = factory;
        }

        protected IAutoReservationService Service { get; private set; }

        public bool ServiceExists
        {
            get { return Service  != null; }
        }

        public void Init()
        {
            Service = factory.GetService();
            Load();
        }

        protected abstract void Load();

        protected bool Validate(IEnumerable<IValidatable> items) 
        {
            var errorText = new StringBuilder();
            foreach (var item in items)
            {
                var error = item.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(item.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        private string errorText;
        public string ErrorText
        {
            get { return errorText; }
            set
            {
                if (errorText == value)
                {
                    return;
                }
                errorText = value;
                this.OnPropertyChanged(p => p.ErrorText);
            }
        }

        #region Helper Methods
        
        void IExtendedNotifyPropertyChanged.OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
