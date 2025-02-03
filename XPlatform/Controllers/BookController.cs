using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XPlatform.DTO;
using XPlatform.Models;



namespace XPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
            var books = _context.Books.Include(b => b.Author)
                .Select(b => new BookDTO
                {
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    IsAvailable = b.IsAvailable()
                }).ToList();

            return Ok(books);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDTO bookDto)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Name == bookDto.AuthorName)
                         ?? new Author { Name = bookDto.AuthorName };

            var book = new Book
            {
                Title = bookDto.Title,
                Author = author
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDTO bookDto)
        {
            var book = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            var author = _context.Authors.FirstOrDefault(a => a.Name == bookDto.AuthorName)
                         ?? new Author { Name = bookDto.AuthorName };

            book.Title = bookDto.Title;
            book.Author = author;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("by-author/{authorName}")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksByAuthor(string authorName)
        {
            var books = _context.Books
                .Where(b => b.Author.Name == authorName)
                .Select(b => new BookDTO
                {
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    IsAvailable = b.IsAvailable()
                }).ToList();

            return Ok(books);
        }

        [HttpGet("by-author/{authorId}")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksByAuthor(int authorId)
        {
            var books = _context.Books
                .Where(b => b.AuthorId == authorId)
                .Select(b => new BookDTO
                {
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    IsAvailable = b.IsAvailable()
                }).ToList();

            return Ok(books);
        }


        [HttpGet("borrowed-by/{visitorId}")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksBorrowedByVisitor(int visitorId)
        {
            var books = _context.Books
                .Where(b => b.BorrowerId == visitorId)
                .Select(b => new BookDTO
                {
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    IsAvailable = b.IsAvailable()
                }).ToList();

            return Ok(books);
        }
    }
}
