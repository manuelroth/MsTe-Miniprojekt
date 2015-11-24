using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void AutosTest()
        {
            List<AutoDto> autos = Target.getAutos();
            Assert.AreEqual(3, autos.Count);
        }

        [TestMethod]
        public void KundenTest()
        {
            List<KundeDto> kunden = Target.getKunden();
            Assert.AreEqual(4, kunden.Count);
        }

        [TestMethod]
        public void ReservationenTest()
        {
            List<ReservationDto> reservations = Target.getReservations();
            Assert.AreEqual(3, reservations.Count);
        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            AutoDto auto = Target.getAuto(1);
            Assert.AreEqual(1, auto.Id);
            Assert.AreEqual("Fiat Punto", auto.Marke);
            Assert.AreEqual(AutoKlasse.Standard, auto.AutoKlasse);
            Assert.AreEqual(50, auto.Tagestarif);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            KundeDto kunde = Target.getKunde(1);
            Assert.AreEqual(1, kunde.Id);
            Assert.AreEqual("Anna", kunde.Vorname);
            Assert.AreEqual("Nass", kunde.Nachname);
            Assert.AreEqual(new DateTime(1961, 5, 5), kunde.Geburtsdatum);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.getReservation(1);
            Assert.AreEqual(1, reservation.ReservationNr);
            Assert.AreEqual(new DateTime(2020, 1, 10), reservation.Von);
            Assert.AreEqual(new DateTime(2020, 1, 20), reservation.Bis);
            Assert.AreEqual(1, reservation.Kunde.Id);
            Assert.AreEqual(1, reservation.Auto.Id);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            ReservationDto reservation = Target.getReservation(7);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            AutoDto auto = new AutoDto();
            auto.AutoKlasse = AutoKlasse.Mittelklasse;
            auto.Tagestarif = 100;
            auto.Marke = "Wiesmann";
            AutoDto resultAuto = Target.addAuto(auto);
            Assert.AreEqual(4, resultAuto.Id);
            Assert.AreEqual(AutoKlasse.Mittelklasse, resultAuto.AutoKlasse);
            Assert.AreEqual(100, resultAuto.Tagestarif);
            Assert.AreEqual("Wiesmann", resultAuto.Marke);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            KundeDto kunde = new KundeDto();
            kunde.Geburtsdatum = new DateTime(1900, 10, 10);
            kunde.Nachname = "Bock";
            kunde.Vorname = "Sebastian";
            KundeDto resultKunde = Target.addKunde(kunde);
            Assert.AreEqual(5, resultKunde.Id);
            Assert.AreEqual(new DateTime(1900, 10, 10), resultKunde.Geburtsdatum);
            Assert.AreEqual("Bock", resultKunde.Nachname);
            Assert.AreEqual("Sebastian", resultKunde.Vorname);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            ReservationDto reservation = new ReservationDto();
            reservation.Von = new DateTime(1900, 10, 10);
            reservation.Bis = new DateTime(1980, 10, 10);
            KundeDto kunde = Target.getKunde(3);
            reservation.Kunde = kunde;
            AutoDto auto = Target.getAuto(2);
            reservation.Auto = auto;
            ReservationDto resultReservation = Target.addReservation(reservation);
            Assert.AreEqual(4, resultReservation.ReservationNr);
            Assert.AreEqual(new DateTime(1900, 10, 10), resultReservation.Von);
            Assert.AreEqual(new DateTime(1980, 10, 10), resultReservation.Bis);
            Assert.AreEqual(3, resultReservation.Kunde.Id);
            Assert.AreEqual(2, resultReservation.Auto.Id);
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            AutoDto originalAuto = Target.getAuto(1);
            AutoDto modifiedAuto = Target.getAuto(1);
            modifiedAuto.Marke = "Bugatti";
            Target.updateAuto(modifiedAuto, originalAuto);
            AutoDto resultAuto = Target.getAuto(1);
            Assert.AreEqual("Bugatti", resultAuto.Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            KundeDto originalKunde = Target.getKunde(1);
            KundeDto modifiedKunde = Target.getKunde(1);
            modifiedKunde.Nachname = "Meili";
            Target.updateKunde(modifiedKunde, originalKunde);
            KundeDto resultKunde = Target.getKunde(1);
            Assert.AreEqual("Meili", resultKunde.Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto originalReservation = Target.getReservation(1);
            ReservationDto modifiedReservation = Target.getReservation(1);
            modifiedReservation.Bis = new DateTime(3000, 11, 11);
            Target.updateReservation(modifiedReservation, originalReservation);
            ReservationDto resultReservation = Target.getReservation(1);
            Assert.AreEqual(new DateTime(3000, 11, 11), resultReservation.Bis);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<UpdateConcurrencyFault>))]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            AutoDto originalAuto = Target.getAuto(1);
            AutoDto modifiedAuto = Target.getAuto(1);
            modifiedAuto.Marke = "Bugatti";

            // Between update
            AutoDto betweenOriginalAuto = Target.getAuto(1);
            AutoDto betweenModifiedAuto = Target.getAuto(1);
            betweenModifiedAuto.Marke = "DeLorean";
            Target.updateAuto(betweenModifiedAuto, betweenOriginalAuto);

            Target.updateAuto(modifiedAuto, originalAuto);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<UpdateConcurrencyFault>))]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            KundeDto originalKunde = Target.getKunde(1);
            KundeDto modifiedKunde = Target.getKunde(1);
            modifiedKunde.Nachname = "Meili";

            // Between update
            KundeDto betweenOriginalKunde = Target.getKunde(1);
            KundeDto betweenModifiedKunde = Target.getKunde(1);
            betweenModifiedKunde.Nachname = "Luemmel";
            Target.updateKunde(betweenModifiedKunde, betweenOriginalKunde);

            Target.updateKunde(modifiedKunde, originalKunde);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<UpdateConcurrencyFault>))]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            ReservationDto originalReservation = Target.getReservation(1);
            ReservationDto modifiedReservation = Target.getReservation(1);
            modifiedReservation.Bis = new DateTime(3000, 11, 11);

            // Between update
            ReservationDto betweenOriginalReservation = Target.getReservation(1);
            ReservationDto betweenModifiedReservation = Target.getReservation(1);
            betweenModifiedReservation.Bis = new DateTime(4000, 11, 11);
            Target.updateReservation(betweenModifiedReservation, betweenOriginalReservation);

            Target.updateReservation(modifiedReservation, originalReservation);
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            AutoDto auto = Target.getAuto(1);
            Target.deleteAuto(auto);
            auto = Target.getAuto(1);
            Assert.IsNull(auto);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            KundeDto kunde = Target.getKunde(1);
            Target.deleteKunde(kunde);
            kunde = Target.getKunde(1);
            Assert.IsNull(kunde);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            ReservationDto reservation = Target.getReservation(1);
            Target.deleteReservation(reservation);
            reservation = Target.getReservation(1);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void DeleteKundeAndGetReservationTest()
        {
            ReservationDto reservation = Target.getReservation(1);
            KundeDto kunde = reservation.Kunde;
            Target.deleteKunde(kunde);
            reservation = Target.getReservation(1);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void DeleteAutoAndGetReservationTest()
        {
            ReservationDto reservation = Target.getReservation(1);
            AutoDto auto = reservation.Auto;
            Target.deleteAuto(auto);
            reservation = Target.getReservation(1);
            Assert.IsNull(reservation);
        }

    }
}
