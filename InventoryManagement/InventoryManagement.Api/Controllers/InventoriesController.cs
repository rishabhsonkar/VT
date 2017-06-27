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
    builder.EntitySet<Inventory>("Inventories");
    builder.EntitySet<Item>("Items"); 
    builder.EntitySet<Location>("Locations"); 
    builder.EntitySet<Organisation>("Organisations"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class InventoriesController : ODataController
    {
        private InventMgmEntities db = new InventMgmEntities();

        // GET: odata/Inventories
        [EnableQuery]
        public IQueryable<Inventory> GetInventories()
        {
            return db.Inventories;
        }

        // GET: odata/Inventories(5)
        [EnableQuery]
        public SingleResult<Inventory> GetInventory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Inventories.Where(inventory => inventory.OrganisationId == key));
        }

        // PUT: odata/Inventories(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Inventory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory inventory = db.Inventories.Find(key);
            if (inventory == null)
            {
                return NotFound();
            }

            patch.Put(inventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inventory);
        }

        // POST: odata/Inventories
        public IHttpActionResult Post(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Inventories.Add(inventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InventoryExists(inventory.OrganisationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(inventory);
        }

        // PATCH: odata/Inventories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Inventory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory inventory = db.Inventories.Find(key);
            if (inventory == null)
            {
                return NotFound();
            }

            patch.Patch(inventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inventory);
        }

        // DELETE: odata/Inventories(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Inventory inventory = db.Inventories.Find(key);
            if (inventory == null)
            {
                return NotFound();
            }

            db.Inventories.Remove(inventory);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Inventories(5)/Item
        [EnableQuery]
        public SingleResult<Item> GetItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.Inventories.Where(m => m.OrganisationId == key).Select(m => m.Item));
        }

        // GET: odata/Inventories(5)/Location
        [EnableQuery]
        public SingleResult<Location> GetLocation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Inventories.Where(m => m.OrganisationId == key).Select(m => m.Location));
        }

        // GET: odata/Inventories(5)/Organisation
        [EnableQuery]
        public SingleResult<Organisation> GetOrganisation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Inventories.Where(m => m.OrganisationId == key).Select(m => m.Organisation));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryExists(int key)
        {
            return db.Inventories.Count(e => e.OrganisationId == key) > 0;
        }
    }
}
