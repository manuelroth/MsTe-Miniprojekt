using System;
using System.Diagnostics;
using AutoReservation.Common.Interfaces;
using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private AutoReservationBusinessComponent autoReservationBusinessComponent;

        public AutoReservationService() {
            autoReservationBusinessComponent = new AutoReservationBusinessComponent();
        }

        // CRUD Auto
        public List<AutoDto> getAutos()
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.getAutos().ConvertToDtos();
        }

        public AutoDto getAuto(int index)
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.getAuto(index).ConvertToDto();
        }

        public AutoDto addAuto(AutoDto auto)
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.addAuto(auto.ConvertToEntity()).ConvertToDto();
        }

        public void updateAuto(AutoDto modified, AutoDto original)
        {
            WriteActualMethod();
            try
            {
                autoReservationBusinessComponent.updateAuto(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Auto> e)
            {
                UpdateConcurrencyFault ucf = new UpdateConcurrencyFault();
                ucf.Message = e.Message;
                throw new FaultException<UpdateConcurrencyFault>(ucf);
            }
        }

        public void deleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            autoReservationBusinessComponent.deleteAuto(auto.ConvertToEntity());
        }


        // CRUD Kunden
        public List<KundeDto> getKunden()
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.getKunden().ConvertToDtos();
        }

        public KundeDto getKunde(int index)
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.getKunde(index).ConvertToDto();
        }

        public KundeDto addKunde(KundeDto kunde)
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.addKunde(kunde.ConvertToEntity()).ConvertToDto();
        }

        public void updateKunde(KundeDto modified, KundeDto original)
        {
            WriteActualMethod();
            try
            {
                autoReservationBusinessComponent.updateKunde(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Kunde> e)
            {
                UpdateConcurrencyFault ucf = new UpdateConcurrencyFault();
                ucf.Message = e.Message;
                throw new FaultException<UpdateConcurrencyFault>(ucf);
            }
        }

        public void deleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            autoReservationBusinessComponent.deleteKunde(kunde.ConvertToEntity());
        }


        // CRUD Reservationen
        public List<ReservationDto> getReservations()
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.getReservations().ConvertToDtos();
        }

        public ReservationDto getReservation(int index)
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.getReservation(index).ConvertToDto();
        }

        public ReservationDto addReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return autoReservationBusinessComponent.addReservation(reservation.ConvertToEntity()).ConvertToDto();
        }

        public void updateReservation(ReservationDto modified, ReservationDto original)
        {
            WriteActualMethod();
            try
            {
                autoReservationBusinessComponent.updateReservation(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Reservation> e)
            {
                UpdateConcurrencyFault ucf = new UpdateConcurrencyFault();
                ucf.Message = e.Message;
                throw new FaultException<UpdateConcurrencyFault>(ucf);
            }
        }

        public void deleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            autoReservationBusinessComponent.deleteReservation(reservation.ConvertToEntity());
        }

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }

    }
}