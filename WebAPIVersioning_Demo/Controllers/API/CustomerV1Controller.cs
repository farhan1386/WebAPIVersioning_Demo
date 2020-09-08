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
    public class CustomerV1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CustomerV1
        public IQueryable<CustomerV1> GetCustomerV1s()
        {
            return db.CustomerV1s;
        }

        // GET: api/CustomerV1/5
        [ResponseType(typeof(CustomerV1))]
        public IHttpActionResult GetCustomerV1(int id)
        {
            CustomerV1 customerV1 = db.CustomerV1s.Find(id);
            if (customerV1 == null)
            {
                return NotFound();
            }

            return Ok(customerV1);
        }

        // PUT: api/CustomerV1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerV1(int id, CustomerV1 customerV1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerV1.Id)
            {
                return BadRequest();
            }

            db.Entry(customerV1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerV1Exists(id))
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

        // POST: api/CustomerV1
        [ResponseType(typeof(CustomerV1))]
        public IHttpActionResult PostCustomerV1(CustomerV1 customerV1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerV1s.Add(customerV1);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerV1.Id }, customerV1);
        }

        // DELETE: api/CustomerV1/5
        [ResponseType(typeof(CustomerV1))]
        public IHttpActionResult DeleteCustomerV1(int id)
        {
            CustomerV1 customerV1 = db.CustomerV1s.Find(id);
            if (customerV1 == null)
            {
                return NotFound();
            }

            db.CustomerV1s.Remove(customerV1);
            db.SaveChanges();

            return Ok(customerV1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerV1Exists(int id)
        {
            return db.CustomerV1s.Count(e => e.Id == id) > 0;
        }
    }
}