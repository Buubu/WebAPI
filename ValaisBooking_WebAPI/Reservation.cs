//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ValaisBooking_WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int IdReservation { get; set; }
        public int IdReservationDetails { get; set; }
        public System.DateTime DateReservation { get; set; }
        public string ClientFirstname { get; set; }
        public string ClientLastname { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateEnd { get; set; }
        public decimal TotalPrice { get; set; }
        public int IdRoom { get; set; }
    
        public virtual ReservationDetail ReservationDetail { get; set; }
    }
}
