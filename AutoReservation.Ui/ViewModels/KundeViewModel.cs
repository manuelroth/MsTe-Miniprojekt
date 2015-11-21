using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Ui.Factory;

namespace AutoReservation.Ui.ViewModels
{
    public class KundeViewModel : ViewModelBase
    {
        public KundeViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<KundeDto> Kunden
        {
            get { throw new NotImplementedException(); }
        }

        public KundeDto SelectedKunde
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        #region Load-Command

        public ICommand LoadCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override void Load()
        {
            
        }

        #endregion

        #region Save-Command

        public ICommand SaveCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }
       
        #endregion

        #region New-Command

        public ICommand NewCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Delete-Command

        public ICommand DeleteCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
