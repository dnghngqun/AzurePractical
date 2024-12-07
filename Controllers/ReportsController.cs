using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Data;

public class ReportsController : Controller
{
    private readonly ComicSystemContext _context;

    public ReportsController(ComicSystemContext context)
    {
        _context = context;
    }

    public IActionResult RentalReport(DateTime? startDate, DateTime? endDate)
    {
        var rentals = _context.Rentals
            .Where(r => (!startDate.HasValue || r.RentalDate >= startDate) &&
                        (!endDate.HasValue || r.RentalDate <= endDate))
            .Include(r => r.Customer)
            .Include(r => r.RentalDetails)
            .ThenInclude(rd => rd.ComicBook)
            .ToList();

        return View(rentals);
    }
}