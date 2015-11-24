using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {

        // CRUD Autos
        public IList<Auto> getAutos()
        {
            using (var context = new AutoReservationEntities())
            {
                var autos = from a in context.Auto select a;
                return autos.ToList();
            }
        }

        public Auto getAuto(int index)
        {
            using (var context = new AutoReservationEntities())
            {
                var autos = from a in context.Auto where a.Id == index select a;
                if (autos.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return autos.First();
                }
            }
        }

        public Auto addAuto(Auto auto)
        {
            using (var context = new AutoReservationEntities())
            {
                Auto addedAuto = context.Auto.Add(auto);
                context.SaveChanges();
                return addedAuto;
            }
        }

        public void updateAuto(Auto modified, Auto original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Auto.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException<Auto>(context, original);
                }
            }
        }

        public void deleteAuto(Auto auto)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Auto.Attach(auto);
                context.Auto.Remove(auto);
                context.SaveChanges();
            }
        }

        // CRUD Kunden
        public IList<Kunde> getKunden()
        {
            using (var context = new AutoReservationEntities())
            {
                var kunden = from k in context.Kunde select k;
                return kunden.ToList();
            }
        }

        public Kunde getKunde(int index)
        {
            using (var context = new AutoReservationEntities())
            {
                var kunden = from k in context.Kunde where k.Id == index select k;
                if (kunden.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return kunden.First();
                }
            }
        }

        public Kunde addKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                Kunde addedKunde = context.Kunde.Add(kunde);
                context.SaveChanges();
                return addedKunde;
            }
        }

        public void updateKunde(Kunde modified, Kunde original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunde.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException<Kunde>(context, original);
                }
            }
        }

        public void deleteKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunde.Attach(kunde);
                context.Kunde.Remove(kunde);
                context.SaveChanges();
            }
        }

        // CRUD Reservations
        public IList<Reservation> getReservations()
        {
            using (var context = new AutoReservationEntities())
            {
                var reservations = from r in context.Reservation.Include("Auto").Include("Kunde") select r;
                return reservations.ToList();
            }
        }

        public Reservation getReservation(int index)
        {
            using (var context = new AutoReservationEntities())
            {
                var reservations = from r in context.Reservation.Include("Auto").Include("Kunde") where r.ReservationsNr == index select r;
                if (reservations.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return reservations.First();
                }
            }
        }

        public Reservation addReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Auto.Attach(getAuto(reservation.AutoId));
                context.Kunde.Attach(getKunde(reservation.KundeId));
                Reservation addedReservation = context.Reservation.Add(reservation);
                context.SaveChanges();
                return addedReservation;
            }
        }

        public void updateReservation(Reservation modified, Reservation original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservation.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException<Reservation>(context, original);
                }
            }
        }

        public void deleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservation.Attach(reservation);
                context.Reservation.Remove(reservation);
                context.SaveChanges();
            }
        }

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }

    }
}