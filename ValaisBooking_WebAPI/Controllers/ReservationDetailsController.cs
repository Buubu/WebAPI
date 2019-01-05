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
    public class ReservationDetailsController : ApiController
    {
        private ValaisBookingEntities1 db = new ValaisBookingEntities1();

        // GET: api/ReservationDetails
        public IQueryable<ReservationDetail> GetReservationDetails()
        {
            return db.ReservationDetails;
        }

        // GET: api/ReservationDetails/5
        [ResponseType(typeof(ReservationDetail))]
        public IHttpActionResult GetReservationDetail(int id)
        {
            ReservationDetail reservationDetail = db.ReservationDetails.Find(id);
            if (reservationDetail == null)
            {
                return NotFound();
            }

            return Ok(reservationDetail);
        }

        // PUT: api/ReservationDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservationDetail(int id, ReservationDetail reservationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservationDetail.IdReservationDetails)
            {
                return BadRequest();
            }

            db.Entry(reservationDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationDetailExists(id))
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


        // POST: api/ReservationDetails
        [ResponseType(typeof(ReservationDetail))]
        [HttpPost]
        public async Task<IHttpActionResult> AddReservationDetails([FromBody]ReservationDetail reservationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReservationDetails.Add(reservationDetail);
			await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservationDetail.IdReservationDetails }, reservationDetail);
        }

        // DELETE: api/ReservationDetails/5
		[HttpDelete]
        [ResponseType(typeof(ReservationDetail))]
        public IHttpActionResult RemoveReservationDetails(int id)
        {
            ReservationDetail reservationDetail = db.ReservationDetails.Find(id);
            if (reservationDetail == null)
            {
                return NotFound();
            }

            db.ReservationDetails.Remove(reservationDetail);
            db.SaveChanges();

            return Ok(reservationDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationDetailExists(int id)
        {
            return db.ReservationDetails.Count(e => e.IdReservationDetails == id) > 0;
        }
    }
}