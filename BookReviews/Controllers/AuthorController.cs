using BookReviews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Controllers;

public class AuthorController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Author
    public async Task<IActionResult> Index()
    {
        return _context.Authors != null
            ? View(await _context.Authors.ToListAsync())
            : Problem("Entity set 'ApplicationDbContext.Authors'  is null.");
    }

    // GET: Author/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Authors == null) return NotFound();

        var author = await _context.Authors
            .FirstOrDefaultAsync(m => m.AuthorId == id);
        if (author == null) return NotFound();

        return View(author);
    }

    // GET: Author/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Author/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AuthorId,Name,Birthdate,BookId")] Author author)
    {
        if (ModelState.IsValid)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(author);
    }

    // GET: Author/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Authors == null) return NotFound();

        var author = await _context.Authors.FindAsync(id);
        if (author == null) return NotFound();
        return View(author);
    }

    // POST: Author/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AuthorId,Name,Birthdate,BookId")] Author author)
    {
        if (id != author.AuthorId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(author.AuthorId))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(author);
    }

    // GET: Author/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Authors == null) return NotFound();

        var author = await _context.Authors
            .FirstOrDefaultAsync(m => m.AuthorId == id);
        if (author == null) return NotFound();

        return View(author);
    }

    // POST: Author/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Authors == null) return Problem("Entity set 'ApplicationDbContext.Authors'  is null.");
        var author = await _context.Authors.FindAsync(id);
        if (author != null) _context.Authors.Remove(author);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Books(int? id)
    {
        if (id == null || _context.Authors == null) return NotFound();
        
        ViewBag.Author = (await _context.Authors.FindAsync(id))?.Name;

        var books = await _context.Books
            .Include(b => b.Authors)
            .Where(b => b.Authors.Any(a => a.AuthorId == id))
            .ToListAsync();

        if (books == null) return NotFound();

        return View(books);
    }

    private bool AuthorExists(int id)
    {
        return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
    }
}