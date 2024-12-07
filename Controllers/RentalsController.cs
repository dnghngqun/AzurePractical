using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Data;
using ComicSystem.Models;

public class RentalsController : Controller
{
    private readonly ComicSystemContext _context;

    public RentalsController(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var rentals = await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.RentalDetails)
            .ThenInclude(rd => rd.ComicBook)
            .ToListAsync();
        return View(rentals);
    }

    public IActionResult Create()
    {
        ViewBag.Customers = new SelectList(_context.Customers, "CustomerID", "Fullname");
        ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookID", "BookName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Rental rental, List<RentalDetail> rentalDetails)
    {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            foreach (var detail in rentalDetails)
            {
                detail.RentalID = rental.RentalID;
                _context.RentalDetails.Add(detail);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
    }

    // GET: Rentals/UpdateStatus/5
    public async Task<IActionResult> UpdateStatus(int? id)
    {
        if (id == null) return NotFound();

        var rental = await _context.Rentals
            .AsNoTracking() // Chỉ truy vấn, không tracking để tránh ảnh hưởng các cột khác.
            .FirstOrDefaultAsync(r => r.RentalID == id);

        if (rental == null) return NotFound();

        ViewBag.StatusOptions = new SelectList(new[] { "Renting", "Returned", "Overdued" }, rental.Status);
        return View(rental);
    }
    // POST: Rentals/UpdateStatus/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, [Bind("Status")] Rental rental)
    {

        try
        {
            var rentalToUpdate = new Rental { RentalID = id }; // Chỉ định đối tượng với RentalID
            _context.Rentals.Attach(rentalToUpdate); // Attach đối tượng mà không tải toàn bộ dữ liệu
            rentalToUpdate.Status = rental.Status; // Cập nhật chỉ cột Status
            await _context.SaveChangesAsync(); // Lưu thay đổi
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Rentals.Any(r => r.RentalID == id)) return NotFound();
            else throw;
        }

        return RedirectToAction(nameof(Index));
    }
}