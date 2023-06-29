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

namespace bookwormapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderModelsController : ControllerBase
    {
        private readonly BookwormContext _context;

        public OrderModelsController(BookwormContext context)
        {
            _context = context;
        }

        // GET: api/OrderModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrderModel()
        {
          if (_context.OrderModel == null)
          {
              return NotFound();
          }
            return await _context.OrderModel.ToListAsync();
        }

        // GET: api/OrderModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrderModel(int id)
        {
          if (_context.OrderModel == null)
          {
              return NotFound();
          }
            var orderModel = await _context.OrderModel.Where(u => (u.UserId == id)).ToListAsync();

            if (orderModel == null)
            {
                return NotFound();
            }

            return orderModel;
        }

        // PUT: api/OrderModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderModel(int id, [FromBody] OrderModelDao orderModel)
        {
            if (id != orderModel.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(id))
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

        // POST: api/OrderModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderModel([FromBody] OrderModelDao orderModelDao)
        {
            OrderModel orderModel = new OrderModel()
            {
                OrderId = orderModelDao.OrderId,
                OrderDuration = orderModelDao.OrderDuration,
                UserId = orderModelDao.UserId,
                OrderStatus = orderModelDao.OrderStatus,
                UserAddress = orderModelDao.UserAddress,
                NumberOfItems = orderModelDao.NumberOfItems,
                TotalCost = orderModelDao.TotalCost,
            };
          if (_context.OrderModel == null)
          {
              return Problem("Entity set 'BookwormContext.OrderModel'  is null.");
          }
            _context.OrderModel.Add(orderModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderModel", new { id = orderModel.OrderId }, orderModel);
        }

        // DELETE: api/OrderModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderModel(int id)
        {
            if (_context.OrderModel == null)
            {
                return NotFound();
            }
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            _context.OrderModel.Remove(orderModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderModelExists(int id)
        {
            return (_context.OrderModel?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
