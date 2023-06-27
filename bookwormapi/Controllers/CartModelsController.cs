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
using NuGet.Versioning;

namespace bookwormapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartModelsController : ControllerBase
    {
        private readonly BookwormContext _context;

        public CartModelsController(BookwormContext context)
        {
            _context = context;
        }

        // GET: api/CartModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartModel>>> GetCartModel()
        {
          if (_context.CartModel == null)
          {
              return NotFound();
          }
            return await _context.CartModel.ToListAsync();
        }


        // GET: api/CartModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CartModel>>> GetCartModel(int id)
        {
          if (_context.CartModel == null)
          {
              return NotFound();
          }
            var cartModel = await _context.CartModel.Where(u => (u.UserId == id)).ToListAsync();

            foreach (var item in cartModel) 
            {
                if (item.User == null)
                {
                    item.User = await _context.UserModel.FindAsync(item.UserId);
                    item.User.Carts = null;
                }

                if (item.Book == null)
                {
                    item.Book = await _context.BookModel.FindAsync(item.BookId);
                    item.Book.Carts = null;
                }
                
            }

            if (cartModel == null)
            {
                return NotFound();
            }

            return cartModel;
        }

        // PUT: api/CartModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartModel(int id,[FromBody] CartModelDao cartModelDao)
        {
            CartModel cartModel = await _context.CartModel.FindAsync(id);
            cartModel.BookQuantity = cartModelDao.BookQuantity;
            
            if (id != cartModel.CartId)
            {
                return BadRequest();
            }

            _context.Entry(cartModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartModelExists(id))
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

        // POST: api/CartModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartModel>> PostCartModel([FromBody] CartModelDao cartModelDao)
        {
            Console.WriteLine("In post method");
            CartModel cartModel = new CartModel()
            {
                CartId = cartModelDao.CartId,
                UserId = cartModelDao.UserId,
                BookId = cartModelDao.BookId,
                BookQuantity = cartModelDao.BookQuantity,
            };

            CartModel? item = await _context.CartModel.Where(b => ((b.BookId == cartModel.BookId) && (b.UserId == cartModel.UserId))).FirstOrDefaultAsync();

            
            if (item != null)
            {
                Console.WriteLine("IN item");
                await EditItemQuantity(item.CartId, 1);
            }
            else
            {
                if (_context.CartModel == null)
                {
                    return Problem("Entity set 'BookwormContext.CartModel'  is null.");
                }
                _context.CartModel.Add(cartModel);
                await _context.SaveChangesAsync();

            }

            return CreatedAtAction("GetCartModel", new { id = cartModel.CartId }, cartModel);
        }

        // DELETE: api/CartModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartModel(int id)
        {
            if (_context.CartModel == null)
            {
                return NotFound();
            }
            var cartModel = await _context.CartModel.FindAsync(id);
            if (cartModel == null)
            {
                return NotFound();
            }

            _context.CartModel.Remove(cartModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartModelExists(int id)
        {
            return (_context.CartModel?.Any(e => e.CartId == id)).GetValueOrDefault();
        }

        [HttpGet("{id}/{val}")]
        public async Task<IActionResult> EditItemQuantity(int id, int val)
        {
            CartModel cart = await _context.CartModel.FindAsync(id);
            if (val == 0 && cart.BookQuantity == 1)
            {
                await DeleteCartModel(id);
            }
            else
            {
                cart.BookQuantity += (val == 1) ? 1 : -1;

                CartModelDao cartModelDao = new CartModelDao()
                {
                    BookQuantity = cart.BookQuantity,
                };
                await PutCartModel(id, cartModelDao);

            }
            return Ok();
            
        }
    }
}
