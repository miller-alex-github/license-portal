using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace License.Portal.Web.Controllers
{
    [Authorize]
    public class LicenseController : Controller
    {
        private readonly ApplicationDbContext context;

        public LicenseController(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IActionResult> Index()
        {
            var licenses = await context.Licenses.ToListAsync();
            var model = new LicenseViewModel { Licenses = licenses };

            return View(model);
        }

        public IActionResult Add()
        {
            return View(model: new LicenseDto 
            {                
                Name = "License (default)",
                Product = "IOTRADIO",
                Package = "SystemManager"                
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] LicenseDto newItem)
        {
            if (ModelState.IsValid)
            {
                newItem.Id = Guid.NewGuid();
                newItem.GeneratedAt = DateTime.UtcNow;
                newItem.Key = Guid.NewGuid().ToString();

                var entity = await context.Licenses.AddAsync(newItem);
                var successful = await context.SaveChangesAsync() == 1;
                entity.State = EntityState.Detached;
                if (!successful)
                    return BadRequest("Could not add item.");

                return RedirectToAction("Index");
            }
            else
            {
                return View("Add", model: newItem);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var license = await context.Licenses.FirstOrDefaultAsync(x => x.Id == id);
            if (license == null)
                return NotFound();

            return View(model: license);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [FromForm] LicenseDto item)
        {
            if (ModelState.IsValid)
            {
                if (!context.Licenses.Any(x => x.Id == item.Id))
                    return NotFound();


                item.Id = id;

                var entity = context.Licenses.Update(item);
                var successful = await context.SaveChangesAsync() == 1;
                entity.State = EntityState.Detached;
                               
                if (!successful)
                    return BadRequest("Could not update the item.");

                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", model: item);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var license = await context.Licenses.FindAsync(id);
                if (license == null)
                    return NotFound();

                var entity = context.Licenses.Remove(license);
                var ok = await context.SaveChangesAsync() == 1;
                entity.State = EntityState.Detached;
            }
            catch
            {
                // ignore
            }

            return RedirectToAction("Index");
        }
    }
}