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
    public class PicturesController : ApiController
    {
        private ValaisBookingEntities1 db = new ValaisBookingEntities1();

        // GET: api/Pictures
        public IQueryable<Picture> GetPictures()
        {
            return db.Pictures;
        }

        // GET: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public IHttpActionResult GetPicture(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return NotFound();
            }

            return Ok(picture);
        }

		// GET: api/Hotel/{id}/Pictures
		[ResponseType(typeof(String))]
		[Route ("api/Hotel/{id}/Pictures")]
		public IHttpActionResult GetAllPictures(int id)
		{
			var Urls = db.Pictures.Where(p => p.Room.IdHotel == id).Select(p => p.Url).ToList().Distinct();

			if (Urls == null)
			{
				return NotFound();
			}

			return Ok(Urls);
		}

		// GET: api/Room/{id}/Pictures
		[ResponseType(typeof(Picture))]
		[Route("api/Room/{id}/Pictures")]
		public IHttpActionResult GetRoomPictures(int id)
		{
			var pictures = db.Pictures.Where(p => p.IdRoom == id);

			if (pictures == null)
			{
				return NotFound();
			}

			return Ok(pictures);
		}

		// PUT: api/Pictures/5
		[ResponseType(typeof(void))]
        public IHttpActionResult PutPicture(int id, Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != picture.IdPicture)
            {
                return BadRequest();
            }

            db.Entry(picture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
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

        // POST: api/Pictures
        [ResponseType(typeof(Picture))]
        public IHttpActionResult PostPicture(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pictures.Add(picture);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PictureExists(picture.IdPicture))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = picture.IdPicture }, picture);
        }

        // DELETE: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public IHttpActionResult DeletePicture(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return NotFound();
            }

            db.Pictures.Remove(picture);
            db.SaveChanges();

            return Ok(picture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PictureExists(int id)
        {
            return db.Pictures.Count(e => e.IdPicture == id) > 0;
        }
    }
}