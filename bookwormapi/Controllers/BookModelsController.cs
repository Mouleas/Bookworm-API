using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bookwormapi.Data;
using bookwormapi.Models;
using Microsoft.AspNetCore.Cors;
using bookwormapi.Dao;

namespace bookwormapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookModelsController : ControllerBase
    {
        string IMAGE_UPLOAD_PATH = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\uploads\books\");
        private readonly BookwormContext _context;

        public BookModelsController(BookwormContext context)
        {
            _context = context;
        }

        // GET: api/BookModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBookModel()
        {
          if (_context.BookModel == null)
          {
              return NotFound();
          }
            return await _context.BookModel.ToListAsync();
        }

        // GET: api/BookModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBookModel(int id)
        {
          if (_context.BookModel == null)
          {
              return NotFound();
          }
            var bookModel = await _context.BookModel.FindAsync(id);

            if (bookModel == null)
            {
                return NotFound();
            }

            return bookModel;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateBook(int id, [FromBody] BookModelDao bookModelDao)
        {

            BookModel bookModel = await _context.BookModel.FindAsync(id);
            bookModel.BookName = bookModelDao.BookName;
            bookModel.BookDescription = bookModelDao.BookDescription;
            bookModel.BookPrice = bookModelDao.BookPrice;
            bookModel.BookAuthor = bookModelDao.BookAuthor;
            bookModel.BookQuantity = bookModelDao.BookQuantity;
            bookModel.BookLanguage = bookModelDao.BookLanguage;
            bookModel.TotalPages = bookModelDao.TotalPages;

            if (id != bookModel.BookId)
            {
                return BadRequest();
            }

            _context.Entry(bookModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookModel", new { id = bookModel.BookId }, bookModel);
        }



        // PUT: api/BookModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{quantity}")]
        public async Task<IActionResult> PutBookModel(int id, int quantity)
        {
            BookModel bookModel = await _context.BookModel.FindAsync(id);
            bookModel.BookQuantity -= quantity;

            if (id != bookModel.BookId)
            {
                return BadRequest();
            }

            _context.Entry(bookModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookModel", new { id = bookModel.BookId }, bookModel);
        }

        // POST: api/BookModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookModel>> PostBookModel([FromForm] BookModelDao bookModelDao)
        {
            BookModel bookModel = new BookModel()
            {
                BookAuthor = bookModelDao.BookAuthor,
                BookName = bookModelDao.BookName,
                BookDescription = bookModelDao.BookDescription,
                BookPrice = bookModelDao.BookPrice,
                BookQuantity = bookModelDao.BookQuantity,
                BookLanguage = bookModelDao.BookLanguage,
                TotalPages = bookModelDao.TotalPages,
                PreviousOwnership = bookModelDao.PreviousOwnership,
                PublisherId = bookModelDao.PublisherId,
            };

            if (_context.BookModel == null)
          {
              return Problem("Entity set 'BookwormContext.BookModel'  is null.");
          }
            string? ext = null;
            if (this.Request.Form.Files.Count > 0)
            {
                // get extension of picture
                ext = Path.GetExtension(this.Request.Form.Files[0].FileName);
                bookModel.BookImg = ext;
            }

            _context.BookModel.Add(bookModel);
            await _context.SaveChangesAsync();

            if (this.Request.Form.Files.Count > 0)
            {
                // Generate name for the file
                int bookId = bookModel.BookId;
                string fileName = Convert.ToString(bookId) + ext;

                // Create path and stream it to the location
                var filePath = @IMAGE_UPLOAD_PATH +fileName;
                using (var stream = System.IO.File.Create(filePath))
                {
                    this.Request.Form.Files[0].CopyTo(stream);
                }
            }

            return CreatedAtAction("GetBookModel", new { id = bookModel.BookId }, bookModel);
        }

        // DELETE: api/BookModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookModel(int id)
        {
            if (_context.BookModel == null)
            {
                return NotFound();
            }
            var bookModel = await _context.BookModel.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }

            _context.BookModel.Remove(bookModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookModelExists(int id)
        {
            return (_context.BookModel?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
