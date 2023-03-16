using BookReviews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Controllers;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Book
    public async Task<IActionResult> Index()
    {
        return _context.Books != null
            ? View(await _context.Books.ToListAsync())
            : Problem("Entity set 'ApplicationDbContext.Books'  is null.");
    }

    // GET: Book/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Books == null) return NotFound();

        var book = await _context.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null) return NotFound();

        return View(book);
    }

    // GET: Book/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Book/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BookId,BookTitle,Isbn,Publisher,PubDate")] string[] Authors,
        Book book)
    {
        if (ModelState.IsValid)
        {
            if (Authors != null)
                // Loop through the list of author names from the create form and add them to the book
                foreach (var authorName in Authors)
                {
                    if (authorName != null)
                    {
                        var authorObject = await _context.Authors.FirstOrDefaultAsync(a => a.Name == authorName);
                        // If the author is already in the database add the object to the book
                        if (authorObject != null)
                        {
                            book.Authors.Add(authorObject);
                        }
                        // If the author wasn't in the database add them to it
                        // and then add the object to the book
                        else
                        {
                            var newAuthor = new Author { Name = authorName };
                            _context.Authors.Add(newAuthor);
                            book.Authors.Add(newAuthor);
                        }
                    }
                }

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }

    // GET: Book/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Books == null) return NotFound();

        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: Book/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BookId,BookTitle,Isbn,Publisher,PubDate")] Book book)
    {
        if (id != book.BookId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }

    // GET: Book/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Books == null) return NotFound();

        var book = await _context.Books
            .FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null) return NotFound();

        return View(book);
    }

    // POST: Book/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Books == null) return Problem("Entity set 'ApplicationDbContext.Books'  is null.");
        var book = await _context.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(m => m.BookId == id);
        if (book != null)
        {
            // Remove Author objects from the book to maintain referential integrity.
            // BookId FKs will be removed from the Author table in the database.
            // Authors will not be removed from the database.
            book.Authors.Clear();
            _context.Books.Remove(book);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id)
    {
        return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
    }
}