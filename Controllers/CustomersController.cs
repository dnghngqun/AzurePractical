using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Data;
using ComicSystem.Models;

public class CustomersController : Controller
{
    private readonly ComicSystemContext _context;

    public CustomersController(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var customers = await _context.Customers.ToListAsync();
        return View(customers);
    }

    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Customer customer)
    {
            customer.RegisterDate = DateTime.Now;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
    }
}