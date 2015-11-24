using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{

    [ServiceContract]
    public interface IAutoReservationService
    {

        // CRUD Autos
        [OperationContract]
        List<AutoDto> getAutos();

        [OperationContract]
        AutoDto getAuto(int index);

        [OperationContract]
        AutoDto addAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(UpdateConcurrencyFault))]
        void updateAuto(AutoDto modified, AutoDto original);

        [OperationContract]
        void deleteAuto(AutoDto auto);


        // CRUD Kunden
        [OperationContract]
        List<KundeDto> getKunden();

        [OperationContract]
        KundeDto getKunde(int index);

        [OperationContract]
        KundeDto addKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(UpdateConcurrencyFault))]
        void updateKunde(KundeDto modified, KundeDto original);

        [OperationContract]
        void deleteKunde(KundeDto kunde);


        // CRUD Reservation
        [OperationContract]
        List<ReservationDto> getReservations();

        [OperationContract]
        ReservationDto getReservation(int index);

        [OperationContract]
        ReservationDto addReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(UpdateConcurrencyFault))]
        void updateReservation(ReservationDto modified, ReservationDto original);

        [OperationContract]
        void deleteReservation(ReservationDto reservation);

    }
}
