using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void GetAutosTest()
        {
            IList<Auto> autos = Target.getAutos();
            Assert.AreEqual(3, autos.Count);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            IList<Kunde> kunden = Target.getKunden();
            Assert.AreEqual(4, kunden.Count);
        }

        [TestMethod]
        public void GetReservationsTest()
        {
            IList<Reservation> reservations = Target.getReservations();
            Assert.AreEqual(3, reservations.Count);
        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            Auto auto = Target.getAuto(1);
            Assert.AreEqual(1, auto.Id);
            Assert.AreEqual("Fiat Punto", auto.Marke);
            Assert.AreEqual(50, auto.Tagestarif);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            Kunde kunde = Target.getKunde(1);
            Assert.AreEqual(1, kunde.Id);
            Assert.AreEqual("Anna", kunde.Vorname);
            Assert.AreEqual("Nass", kunde.Nachname);
            Assert.AreEqual(new DateTime(1961, 5, 5), kunde.Geburtsdatum);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            Reservation reservation = Target.getReservation(1);
            Assert.AreEqual(1, reservation.ReservationsNr);
            Assert.AreEqual(new DateTime(2020, 1, 10), reservation.Von);
            Assert.AreEqual(new DateTime(2020, 1, 20), reservation.Bis);
            Assert.AreEqual(1, reservation.Kunde.Id);
            Assert.AreEqual(1, reservation.Auto.Id);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            Reservation reservation = Target.getReservation(7);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            Auto auto = new MittelKlasseAuto();
            auto.Tagestarif = 100;
            auto.Marke = "Wiesmann";
            Auto resultAuto = Target.addAuto(auto);
            Assert.AreEqual(4, resultAuto.Id);
            Assert.AreEqual(100, resultAuto.Tagestarif);
            Assert.AreEqual("Wiesmann", resultAuto.Marke);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            Kunde kunde = new Kunde();
            kunde.Geburtsdatum = new DateTime(1900, 10, 10);
            kunde.Nachname = "Bock";
            kunde.Vorname = "Sebastian";
            Kunde resultKunde = Target.addKunde(kunde);
            Assert.AreEqual(5, resultKunde.Id);
            Assert.AreEqual(new DateTime(1900, 10, 10), resultKunde.Geburtsdatum);
            Assert.AreEqual("Bock", resultKunde.Nachname);
            Assert.AreEqual("Sebastian", resultKunde.Vorname);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            Reservation reservation = new Reservation();
            reservation.Von = new DateTime(1900, 10, 10);
            reservation.Bis = new DateTime(1980, 10, 10);
            Kunde kunde = Target.getKunde(3);
            reservation.KundeId = kunde.Id;
            Auto auto = Target.getAuto(2);
            reservation.AutoId = auto.Id;
            Reservation resultReservation = Target.addReservation(reservation);
            Assert.AreEqual(4, resultReservation.ReservationsNr);
            Assert.AreEqual(new DateTime(1900, 10, 10), resultReservation.Von);
            Assert.AreEqual(new DateTime(1980, 10, 10), resultReservation.Bis);
            Assert.AreEqual(3, resultReservation.Kunde.Id);
            Assert.AreEqual(2, resultReservation.Auto.Id);
        }
        
        [TestMethod]
        public void SuccesfulUpdateAutoTest()
        {
            Auto originalAuto = Target.getAuto(1);
            Auto modifiedAuto = Target.getAuto(1);
            modifiedAuto.Marke = "Pagani";
            Target.updateAuto(modifiedAuto, originalAuto);
            Auto resultAuto = Target.getAuto(1);
            Assert.AreEqual("Pagani", resultAuto.Marke);
        }

        [TestMethod]
        public void SuccesfulUpdateKundeTest()
        {
            Kunde originalKunde = Target.getKunde(1);
            Kunde modifiedKunde = Target.getKunde(1);
            modifiedKunde.Nachname = "Buenzli";
            Target.updateKunde(modifiedKunde, originalKunde);
            Kunde resultKunde = Target.getKunde(1);
            Assert.AreEqual("Buenzli", resultKunde.Nachname);
        }

        [TestMethod]
        public void SuccesfulUpdateReservationTest()
        {
            Reservation originalReservation = Target.getReservation(1);
            Reservation modifiedReservation = Target.getReservation(1);
            modifiedReservation.Von = new DateTime(1900, 1, 1);
            Target.updateReservation(modifiedReservation, originalReservation);
            Reservation resultReservation = Target.getReservation(1);
            Assert.AreEqual(new DateTime(1900, 1, 1), resultReservation.Von);
        }

        [TestMethod]
        [ExpectedException(typeof(LocalOptimisticConcurrencyException<Auto>))]
        public void FailedUpdateAutoTest()
        {
            Auto originalAuto = Target.getAuto(1);
            Auto modifiedAuto = Target.getAuto(1);
            modifiedAuto.Marke = "Pagani";

            // Between update
            Auto betweenOriginalAuto = Target.getAuto(1);
            Auto betweenModifiedAuto = Target.getAuto(1);
            betweenModifiedAuto.Marke = "Lamborgini";
            Target.updateAuto(betweenModifiedAuto, betweenOriginalAuto);

            Target.updateAuto(modifiedAuto, originalAuto);
            
        }

        [TestMethod]
        [ExpectedException(typeof(LocalOptimisticConcurrencyException<Kunde>))]
        public void FailedUpdateKundeTest()
        {
            Kunde originalKunde = Target.getKunde(1);
            Kunde modifiedKunde = Target.getKunde(1);
            modifiedKunde.Nachname = "Buenzli";

            // Between Update
            Kunde betweenOriginalKunde = Target.getKunde(1);
            Kunde betweenModifiedKunde = Target.getKunde(1);
            betweenModifiedKunde.Nachname = "Noergler";
            Target.updateKunde(betweenModifiedKunde, betweenOriginalKunde);

            Target.updateKunde(modifiedKunde, originalKunde);

        }

        [TestMethod]
        [ExpectedException(typeof(LocalOptimisticConcurrencyException<Reservation>))]
        public void FailedUpdateReservationTest()
        {
            Reservation originalReservation = Target.getReservation(1);
            Reservation modifiedReservation = Target.getReservation(1);
            modifiedReservation.Bis = new DateTime(3000, 11, 11);

            // Between update
            Reservation betweenOriginalReservation = Target.getReservation(1);
            Reservation betweenModifiedReservation = Target.getReservation(1);
            betweenModifiedReservation.Bis = new DateTime(4000, 11, 11);
            Target.updateReservation(betweenModifiedReservation, betweenOriginalReservation);

            Target.updateReservation(modifiedReservation, originalReservation);
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            Auto auto = Target.getAuto(1);
            Target.deleteAuto(auto);
            auto = Target.getAuto(1);
            Assert.IsNull(auto);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Kunde kunde = Target.getKunde(1);
            Target.deleteKunde(kunde);
            kunde = Target.getKunde(1);
            Assert.IsNull(kunde);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Reservation reservation = Target.getReservation(1);
            Target.deleteReservation(reservation);
            reservation = Target.getReservation(1);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void DeleteKundeAndGetReservationTest()
        {
            Reservation reservation = Target.getReservation(1);
            Kunde kunde = reservation.Kunde;
            Target.deleteKunde(kunde);
            reservation = Target.getReservation(1);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void DeleteAutoAndGetReservationTest()
        {
            Reservation reservation = Target.getReservation(1);
            Auto auto = reservation.Auto;
            Target.deleteAuto(auto);
            reservation = Target.getReservation(1);
            Assert.IsNull(reservation);
        }

    }
}
