using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
    public class RentalDetailsController : Controller
    {
        private readonly ComicSystemContext _context;

        public RentalDetailsController(ComicSystemContext context)
        {
            _context = context;
        }

        // GET: RentalDetails
        public async Task<IActionResult> Index()
        {
            var rentalDetails = _context.RentalDetails
                .Include(rd => rd.Rental)
                .Include(rd => rd.ComicBook);
            return View(await rentalDetails.ToListAsync());
        }

        // GET: RentalDetails/Create
        public IActionResult Create()
        {
            ViewBag.RentalID = new SelectList(_context.Rentals, "RentalID", "RentalID");
            ViewBag.ComicBookID = new SelectList(_context.ComicBooks, "ComicBookID", "BookName");
            return View();
        }

        // POST: RentalDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalDetailID,RentalID,ComicBookID,Quantity,PricePerDay")] RentalDetail rentalDetail)
        {
                _context.Add(rentalDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: RentalDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rentalDetail = await _context.RentalDetails.FindAsync(id);
            if (rentalDetail == null) return NotFound();

            ViewBag.RentalID = new SelectList(_context.Rentals, "RentalID", "RentalID", rentalDetail.RentalID);
            ViewBag.ComicBookID = new SelectList(_context.ComicBooks, "ComicBookID", "BookName", rentalDetail.ComicBookID);
            return View(rentalDetail);
        }

        // POST: RentalDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalDetailID,RentalID,ComicBookID,Quantity,PricePerDay")] RentalDetail rentalDetail)
        {
            if (id != rentalDetail.RentalDetailID) return NotFound();

                try
                {
                    _context.Update(rentalDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalDetailExists(rentalDetail.RentalDetailID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
        }

        // GET: RentalDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rentalDetail = await _context.RentalDetails
                .Include(rd => rd.Rental)
                .Include(rd => rd.ComicBook)
                .FirstOrDefaultAsync(m => m.RentalDetailID == id);

            if (rentalDetail == null) return NotFound();

            return View(rentalDetail);
        }

        // POST: RentalDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var rentalDetail = await _context.RentalDetails.FindAsync(id);
            _context.RentalDetails.Remove(rentalDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalDetailExists(int id)
        {
            return _context.RentalDetails.Any(e => e.RentalDetailID == id);
        }
    }
}