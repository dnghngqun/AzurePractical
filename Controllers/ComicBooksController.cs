using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using ComicSystem.Data;
using ComicSystem.Models;

public class ComicBooksController : Controller
{
    private readonly ComicSystemContext _context;

    public ComicBooksController(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var comicBooks = await _context.ComicBooks.ToListAsync();
        return View(comicBooks);
    }

    public IActionResult Create() => View("Create");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ComicBook comicBook)
    {
            _context.ComicBooks.Add(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var comicBook = await _context.ComicBooks.FindAsync(id);
        if (comicBook == null) return NotFound();
        return View(comicBook);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ComicBook comicBook)
    {
            _context.ComicBooks.Update(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var comicBook = await _context.ComicBooks.FindAsync(id);
        if (comicBook == null) return NotFound();
        return View(comicBook);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var comicBook = await _context.ComicBooks.FindAsync(id);
        if (comicBook != null)
        {
            _context.ComicBooks.Remove(comicBook);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}