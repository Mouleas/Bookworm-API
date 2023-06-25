using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bookwormapi.Data;
using bookwormapi.Models;
using bookwormapi.Dao;
using System.Collections;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using NuGet.Protocol;

namespace bookwormapi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewModelsController : ControllerBase
    {
        private readonly BookwormContext _context;

        public ReviewModelsController(BookwormContext context)
        {
            _context = context;
        }

        // GET: api/ReviewModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewModel>>> GetReviewModel()
        {
          if (_context.ReviewModel == null)
          {
              return NotFound();
          }
            var reviews = await _context.ReviewModel.ToListAsync();

            return reviews;
        }

        // GET: api/ReviewModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewModel>> GetReviewModel(int id)
        {
          if (_context.ReviewModel == null)
          {
              return NotFound();
          }
            var reviewModel = await _context.ReviewModel.FindAsync(id);

            if (reviewModel == null)
            {
                return NotFound();
            }

            return reviewModel;
        }

        // PUT: api/ReviewModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviewModel(int id, [FromBody] ReviewModelDao ReviewDao)
        {
            ReviewModel reviewModel = await _context.ReviewModel.FindAsync(id);
            reviewModel.Review = ReviewDao.Review;
            if (id != reviewModel.ReviewId)
            {
                return BadRequest();
            }

            _context.Entry(reviewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReviewModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReviewModel>> PostReviewModel([FromBody] ReviewModelDao ReviewDao)
        {
            ReviewModel reviewModel = new ReviewModel()
            {
                ReviewId = ReviewDao.ReviewId,
                UserId = ReviewDao.UserId,
                BookId = ReviewDao.BookId,
                Review = ReviewDao.Review,
            };

          if (_context.ReviewModel == null)
          {
              return Problem("Entity set 'BookwormContext.ReviewModel'  is null.");
          }
            _context.ReviewModel.Add(reviewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviewModel", new { id = reviewModel.ReviewId }, reviewModel);
        }

        // DELETE: api/ReviewModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewModel(int id)
        {
            if (_context.ReviewModel == null)
            {
                return NotFound();
            }
            var reviewModel = await _context.ReviewModel.FindAsync(id);
            if (reviewModel == null)
            {
                return NotFound();
            }

            _context.ReviewModel.Remove(reviewModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewModelExists(int id)
        {
            return (_context.ReviewModel?.Any(e => e.ReviewId == id)).GetValueOrDefault();
        }
    }
}
