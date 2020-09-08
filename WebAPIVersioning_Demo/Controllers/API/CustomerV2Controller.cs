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
using WebAPIVersioning_Demo.Data;
using WebAPIVersioning_Demo.Models;

namespace WebAPIVersioning_Demo.Controllers.API
{
    public class CustomerV2Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CustomerV2
        public IQueryable<CustomerV2> GetcustomerV2s()
        {
            return db.customerV2s;
        }

        // GET: api/CustomerV2/5
        [ResponseType(typeof(CustomerV2))]
        public IHttpActionResult GetCustomerV2(int id)
        {
            CustomerV2 customerV2 = db.customerV2s.Find(id);
            if (customerV2 == null)
            {
                return NotFound();
            }

            return Ok(customerV2);
        }

        // PUT: api/CustomerV2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerV2(int id, CustomerV2 customerV2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerV2.Id)
            {
                return BadRequest();
            }

            db.Entry(customerV2).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerV2Exists(id))
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

        // POST: api/CustomerV2
        [ResponseType(typeof(CustomerV2))]
        public IHttpActionResult PostCustomerV2(CustomerV2 customerV2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customerV2s.Add(customerV2);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerV2.Id }, customerV2);
        }

        // DELETE: api/CustomerV2/5
        [ResponseType(typeof(CustomerV2))]
        public IHttpActionResult DeleteCustomerV2(int id)
        {
            CustomerV2 customerV2 = db.customerV2s.Find(id);
            if (customerV2 == null)
            {
                return NotFound();
            }

            db.customerV2s.Remove(customerV2);
            db.SaveChanges();

            return Ok(customerV2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerV2Exists(int id)
        {
            return db.customerV2s.Count(e => e.Id == id) > 0;
        }
    }
}