using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Ui.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReservation.Ui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {

        private readonly List<ReservationDto> reservationsOriginal = new List<ReservationDto>();
        private ObservableCollection<AutoDto> autos;
        private ObservableCollection<KundeDto> kunden;
        private ObservableCollection<ReservationDto> reservations;
        private AutoDto selectedAuto;
        private KundeDto selectedKunde;
        private ReservationDto selectedReservation;

        public ObservableCollection<AutoDto> Autos
        {
            get
            {
                if (autos == null)
                {
                    autos = new ObservableCollection<AutoDto>();
                }
                return autos;
            }
        }

        public ObservableCollection<KundeDto> Kunden
        {
            get
            {
                if (kunden == null)
                {
                    kunden = new ObservableCollection<KundeDto>();
                }
                return kunden;
            }
        }
        
        public ObservableCollection<ReservationDto> Reservations
        {
            get
            {
                if (reservations == null)
                {
                    reservations = new ObservableCollection<ReservationDto>();
                }
                return reservations;
            }
        }

        public AutoDto SelectedAuto
        {
            get
            {
                return selectedAuto;
            }
            set
            {
                if (selectedAuto != value)
                {
                    selectedAuto = value;
                    RaisePropertyChanged();
                }
            }
        }

        public KundeDto SelectedKunde
        {
            get
            {
                return selectedKunde;
            }
            set
            {
                if (selectedKunde != value)
                {
                    selectedKunde = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ReservationDto SelectedReservation
        {
            get
            {
                return selectedReservation;
            }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    RaisePropertyChanged();
                }
            }
        }

        #region Load-Command

        private RelayCommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new RelayCommand(
                        param => Load(),
                        param => CanLoad()
                    );
                }
                return loadCommand;
            }
        }

        protected override void Load()
        {
            Reservations.Clear();
            Autos.Clear();
            Kunden.Clear();
            reservationsOriginal.Clear();
            foreach (AutoDto auto in Service.getAutos())
            {
                Autos.Add(auto);
            }
            foreach (KundeDto kunde in Service.getKunden())
            {
                Kunden.Add(kunde);
            }
            foreach (ReservationDto reservation in Service.getReservations())
            {
                AutoDto auto = Autos.First(item => item.Id == reservation.Auto.Id);
                reservation.Auto = auto;
                KundeDto kunde = Kunden.First(item => item.Id == reservation.Kunde.Id);
                reservation.Kunde = kunde;
                Reservations.Add(reservation);
                reservationsOriginal.Add((ReservationDto)reservation.Clone());
            }
            SelectedReservation = Reservations.FirstOrDefault();
            SelectedAuto = SelectedReservation.Auto;
            SelectedKunde = SelectedReservation.Kunde;
        }

        private bool CanLoad()
        {
            return Service != null;
        }

        #endregion

        #region Save-Command

        private RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(
                        param => SaveData(),
                        param => CanSaveData()
                    );
                }
                return saveCommand;
            }
        }

        private void SaveData()
        {
            foreach (ReservationDto reservation in Reservations)
            {
                if (reservation.ReservationNr == default(int))
                {
                    Service.addReservation(reservation);
                }
                else
                {
                    ReservationDto original = reservationsOriginal.FirstOrDefault(ao => ao.ReservationNr == reservation.ReservationNr);
                    Service.updateReservation(reservation, original);
                }
            }
            Load();
        }

        private bool CanSaveData()
        {
            if (Service == null)
            {
                return false;
            }

            StringBuilder errorText = new StringBuilder();
            foreach (ReservationDto reservation in Reservations)
            {
                string error = reservation.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(reservation.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        #endregion

        #region New-Command

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(
                        param => New(),
                        param => CanNew()
                    );
                }
                return newCommand;
            }
        }

        private void New()
        {

            Reservations.Add(new ReservationDto() { Von = DateTime.Today, Bis = DateTime.Today });
        }

        private bool CanNew()
        {
            return Service != null;
        }

        #endregion

        #region Delete-Command

        private RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(
                        param => Delete(),
                        param => CanDelete()
                    );
                }
                return deleteCommand;
            }
        }

        private void Delete()
        {
            Service.deleteReservation(SelectedReservation);
            Load();
        }

        private bool CanDelete()
        {
            return
                SelectedReservation != null &&
                SelectedReservation.ReservationNr != default(int) &&
                Service != null;
        }

        #endregion

    }
}
