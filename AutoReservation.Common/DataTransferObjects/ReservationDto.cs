using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto : DtoBase
    {

        private DateTime von;
        private DateTime bis;
        private int reservationnr;
        private AutoDto auto;
        private KundeDto kunde;

        [DataMember]
        public DateTime Von
        {
            get { return von; }
            set
            {
                if (von != value)
                {
                    von = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public DateTime Bis
        {
            get { return bis; }
            set
            {
                if (bis != value)
                {
                    bis = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public int ReservationNr
        {
            get { return reservationnr; }
            set
            {
                if (reservationnr != value)
                {
                    reservationnr = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public AutoDto Auto
        {
            get { return auto; }
            set
            {
                if (auto != value)
                {
                    auto = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public KundeDto Kunde
        {
            get { return kunde; }
            set
            {
                if (kunde != value)
                {
                    kunde = value;
                    RaisePropertyChanged();
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (von == DateTime.MinValue)
            {
                error.AppendLine("- von-datum ist nicht gesetzt.");
            }
            if (Bis == DateTime.MinValue)
            {
                error.AppendLine("- bis-datum ist nicht gesetzt.");
            }
            if (Von > Bis)
            {
                error.AppendLine("- von-datum ist grösser als bis-datum.");
            }
            if (Auto == null)
            {
                error.AppendLine("- auto ist nicht zugewiesen.");
            }
            else
            {
                string autoerror = Auto.Validate();
                if (!string.IsNullOrEmpty(autoerror))
                {
                    error.AppendLine(autoerror);
                }
            }
            if (Kunde == null)
            {
                error.AppendLine("- kunde ist nicht zugewiesen.");
            }
            else
            {
                string kundeerror = Kunde.Validate();
                if (!string.IsNullOrEmpty(kundeerror))
                {
                    error.AppendLine(kundeerror);
                }
            }


            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new ReservationDto
            {
                ReservationNr = ReservationNr,
                Von = Von,
                Bis = Bis,
                Auto = (AutoDto)Auto.Clone(),
                Kunde = (KundeDto)Kunde.Clone()
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                ReservationNr,
                Von,
                Bis,
                Auto,
                Kunde);
        }

    }
}
