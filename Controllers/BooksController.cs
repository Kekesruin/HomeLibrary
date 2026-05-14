using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HomeLibrary.Data;
using HomeLibrary.Models;

namespace HomeLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        
        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .FromSqlRaw("EXEC GetAllBooks")
                .ToListAsync();
            
            return View(books);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                // Параметры для хранимой процедуры InsertBook
                var parameters = new[]
                {
                    new SqlParameter("@Title", book.Title ?? (object)DBNull.Value),
                    new SqlParameter("@Author", book.Author ?? (object)DBNull.Value),
                    new SqlParameter("@Year", book.Year ?? (object)DBNull.Value),
                    new SqlParameter("@Genre", book.Genre ?? (object)DBNull.Value),
                    new SqlParameter("@Publisher", book.Publisher ?? (object)DBNull.Value),
                    new SqlParameter("@ISBN", book.ISBN ?? (object)DBNull.Value),
                    new SqlParameter("@ContentHtml", book.ContentHtml ?? (object)DBNull.Value)
                };
                
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertBook @Title, @Author, @Year, @Genre, @Publisher, @ISBN, @ContentHtml",
                    parameters
                );
                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            // Вызов хранимой процедуры GetBookById
            var books = await _context.Books
                .FromSqlRaw("EXEC GetBookById @BookId", new SqlParameter("@BookId", id))
                .ToListAsync();
            
            var book = books.FirstOrDefault();
            if (book == null) return NotFound();
            
            return View(book);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                    new SqlParameter("@BookId", id),
                    new SqlParameter("@Title", book.Title ?? (object)DBNull.Value),
                    new SqlParameter("@Author", book.Author ?? (object)DBNull.Value),
                    new SqlParameter("@Year", book.Year ?? (object)DBNull.Value),
                    new SqlParameter("@Genre", book.Genre ?? (object)DBNull.Value),
                    new SqlParameter("@Publisher", book.Publisher ?? (object)DBNull.Value),
                    new SqlParameter("@ISBN", book.ISBN ?? (object)DBNull.Value),
                    new SqlParameter("@ContentHtml", book.ContentHtml ?? (object)DBNull.Value)
                };
                
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateBook @BookId, @Title, @Author, @Year, @Genre, @Publisher, @ISBN, @ContentHtml",
                    parameters
                );
                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var books = await _context.Books
                .FromSqlRaw("EXEC GetBookById @BookId", new SqlParameter("@BookId", id))
                .ToListAsync();
            
            var book = books.FirstOrDefault();
            if (book == null) return NotFound();
            
            return View(book);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteBook @BookId",
                new SqlParameter("@BookId", id)
            );
            
            return RedirectToAction(nameof(Index));
        }
    }
}
