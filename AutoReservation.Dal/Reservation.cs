//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoReservation.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int ReservationsNr { get; set; }
        public int AutoId { get; set; }
        public int KundeId { get; set; }
        public System.DateTime Von { get; set; }
        public System.DateTime Bis { get; set; }
    
        public virtual Auto Auto { get; set; }
        public virtual Kunde Kunde { get; set; }
    }
}
