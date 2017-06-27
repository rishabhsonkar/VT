using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using InventoryManagement.Data;

namespace InventoryManagement.Api.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using InventoryManagement.Data;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Organisation>("Organisations");
    builder.EntitySet<Inventory>("Inventories"); 
    builder.EntitySet<Item>("Items"); 
    builder.EntitySet<Location>("Locations"); 
    builder.EntitySet<Transaction>("Transactions"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganisationsController : ODataController
    {
        private InventMgmEntities db = new InventMgmEntities();

        // GET: odata/Organisations
        [EnableQuery]
        public IQueryable<Organisation> GetOrganisations()
        {
            return db.Organisations;
        }

        // GET: odata/Organisations(5)
        [EnableQuery]
        public SingleResult<Organisation> GetOrganisation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Organisations.Where(organisation => organisation.OrganisationId == key));
        }

        // PUT: odata/Organisations(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Organisation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organisation organisation = db.Organisations.Find(key);
            if (organisation == null)
            {
                return NotFound();
            }

            patch.Put(organisation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organisation);
        }

        // POST: odata/Organisations
        public IHttpActionResult Post(Organisation organisation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organisations.Add(organisation);
            db.SaveChanges();

            return Created(organisation);
        }

        // PATCH: odata/Organisations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Organisation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organisation organisation = db.Organisations.Find(key);
            if (organisation == null)
            {
                return NotFound();
            }

            patch.Patch(organisation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organisation);
        }

        // DELETE: odata/Organisations(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Organisation organisation = db.Organisations.Find(key);
            if (organisation == null)
            {
                return NotFound();
            }

            db.Organisations.Remove(organisation);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Organisations(5)/Inventories
        [EnableQuery]
        public IQueryable<Inventory> GetInventories([FromODataUri] int key)
        {
            return db.Organisations.Where(m => m.OrganisationId == key).SelectMany(m => m.Inventories);
        }

        // GET: odata/Organisations(5)/Items
        [EnableQuery]
        public IQueryable<Item> GetItems([FromODataUri] int key)
        {
            return db.Organisations.Where(m => m.OrganisationId == key).SelectMany(m => m.Items);
        }

        // GET: odata/Organisations(5)/Locations
        [EnableQuery]
        public IQueryable<Location> GetLocations([FromODataUri] int key)
        {
            return db.Organisations.Where(m => m.OrganisationId == key).SelectMany(m => m.Locations);
        }

        // GET: odata/Organisations(5)/Transactions
        [EnableQuery]
        public IQueryable<Transaction> GetTransactions([FromODataUri] int key)
        {
            return db.Organisations.Where(m => m.OrganisationId == key).SelectMany(m => m.Transactions);
        }

        // GET: odata/Organisations(5)/Users
        [EnableQuery]
        public IQueryable<User> GetUsers([FromODataUri] int key)
        {
            return db.Organisations.Where(m => m.OrganisationId == key).SelectMany(m => m.Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganisationExists(int key)
        {
            return db.Organisations.Count(e => e.OrganisationId == key) > 0;
        }
    }
}
