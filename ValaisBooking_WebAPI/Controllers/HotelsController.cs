using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ValaisBooking_WebAPI;

namespace ValaisBooking_WebAPI.Models
{
    public class HotelsController : ApiController
    {
        private ValaisBookingEntities1 db = new ValaisBookingEntities1();

        // GET: api/Hotels
        public IQueryable<Hotel> GetHotels()
        {
            return db.Hotels;
        }

        // GET: api/Hotels/5
        [ResponseType(typeof(Hotel))]
		[Route("api/Hotels/{id}")]
		public IHttpActionResult GetHotel(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

		// GET: api/Hotels/5/Rooms
		[ResponseType(typeof(Hotel))]
		[Route("api/Hotel/{id}/Rooms")]
		public IHttpActionResult GetRoomsFromHotel(int id)
		{
			Hotel hotel = db.Hotels.Where(h => h.IdHotel == id).Include(h => h.Rooms).FirstOrDefault();
			if (hotel == null)
			{
				return NotFound();
			}

			return Ok(hotel);
		}

		// GET: api/Hotels/sion
		[ResponseType(typeof(Hotel))]
		[Route ("api/Hotels/{location}")]
		public IList<Hotel> GetHotelsSimple(string location)
		{
			var hotels = db.Hotels.Where(h=>h.Location == location).ToList();
			return hotels;
		}

		// PUT: api/Hotels/5
		[ResponseType(typeof(void))]
        public IHttpActionResult PutHotel(int id, Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.IdHotel)
            {
                return BadRequest();
            }

            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult PostHotel(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotels.Add(hotel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HotelExists(hotel.IdHotel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hotel.IdHotel }, hotel);
        }

        // DELETE: api/Hotels/5
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult DeleteHotel(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hotels.Remove(hotel);
            db.SaveChanges();

            return Ok(hotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelExists(int id)
        {
            return db.Hotels.Count(e => e.IdHotel == id) > 0;
        }
    }
}