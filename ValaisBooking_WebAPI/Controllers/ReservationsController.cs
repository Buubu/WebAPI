using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ValaisBooking_WebAPI;

namespace ValaisBooking_WebAPI.Models
{
    public class ReservationsController : ApiController
    {
        private ValaisBookingEntities1 db = new ValaisBookingEntities1();

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
		}


		//// GET: api/Reservations/5
		//      [HttpGet]
		//[ResponseType(typeof(String))]
		//[Route ("api/Reservation/LoginValidation/{idReservation}/{firstname}/{lastname}")]
		//public String LoginReservation(int idReservation, string firstname, string lastname)
		//{
		//          var reservation = db.Reservations.Where(r => r.IdReservation == idReservation
		//              && r.ClientFirstname == firstname
		//              && r.ClientLastname == lastname);

		//          if (!reservation.Any())
		//	{
		//		return "false";
		//	}

		//          return "true";                
		//}

		// GET: api/Reservations/5
		[HttpGet]
		[ResponseType(typeof(String))]
		[Route("api/Reservation/LoginValidation/{idReservation}/{firstname}/{lastname}")]
		public String LoginReservation(int idReservation, string firstname, string lastname)
		{
			var reservation = db.Reservations.Where(r => r.IdReservation == idReservation).Where(r => r.ClientFirstname == firstname).Where(r => r.ClientLastname == lastname).FirstOrDefault();

			if (reservation == null)
			{
				return "false";
			}

			return "true";
		}

		// PUT: api/Reservations/5
		[ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.IdReservation)
            {
                return BadRequest();
            }

            db.Entry(reservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult AddReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservation.IdReservation }, reservation);
        }

		// DELETE: api/Reservations/5
		[HttpDelete]
		[ResponseType(typeof(Reservation))]
        public async Task<IHttpActionResult> RemoveReservation(int id)
        {
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }


			var resDet = db.ReservationDetails.Where(r => r.IdReservation == id).ToList();
			foreach (var rd in resDet)
				db.ReservationDetails.Remove(rd);
            db.Reservations.Remove(reservation);
            await db.SaveChangesAsync();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.IdReservation == id) > 0;
        }
    }
}