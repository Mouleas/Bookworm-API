﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bookwormapi.Data;
using bookwormapi.Models;
using bookwormapi.Dao;
using bookwormapi.SendgridAPI;

namespace bookwormapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsModelsController : ControllerBase
    {
        private readonly BookwormContext _context;

        public OrderItemsModelsController(BookwormContext context)
        {
            _context = context;
        }

        // GET: api/OrderItemsModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemsModel>>> GetOrderItemsModel()
        {
          if (_context.OrderItemsModel == null)
          {
              return NotFound();
          }
            return await _context.OrderItemsModel.ToListAsync();
        }

        // GET: api/OrderItemsModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderItemsModel>>> GetOrderItemsModel(int id)
        {
          if (_context.OrderItemsModel == null)
          {
              return NotFound();
          }
            var orderItemsModel = await _context.OrderItemsModel.Where(o => (o.OrderId == id)).ToListAsync();

            //foreach (var orderItem in orderItemsModel)
            //{
            //    orderItem.Book = await _context.BookModel.FindAsync(orderItem.ProductId);
            //    orderItem.Book.OrderItems = null;
            //}


            if (orderItemsModel == null)
            {
                return NotFound();
            }

            return orderItemsModel;
        }

        // PUT: api/OrderItemsModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{quantity}")]
        public async Task<IActionResult> PutOrderItemsModel(int id, int quantity)
        {
            OrderItemsModel orderItemsModel = await _context.OrderItemsModel.FindAsync(id);
            orderItemsModel.BookQuantity -= quantity;
            if (id != orderItemsModel.OrderItemsId)
            {
                return BadRequest();
            }

            _context.Entry(orderItemsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemsModelExists(id))
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


        [HttpPost("orderItems/")]
        public async Task<ActionResult> PostAllOrderItemsModel([FromBody] List<OrderItemsModelDao> allOrderItemsModelDao)
        {
            new Email().sendOrderDetails(allOrderItemsModelDao);
            foreach (var orderItemsModelDao in allOrderItemsModelDao)
            {
                await PostOrderItemsModel(orderItemsModelDao);
            }
            
            return Ok();
        }


        // POST: api/OrderItemsModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderItemsModel>> PostOrderItemsModel([FromBody] OrderItemsModelDao orderItemsModelDao)
        {
            OrderItemsModel orderItemsModel = new OrderItemsModel()
            {
                OrderItemsId = orderItemsModelDao.OrderItemsId,
                OrderId = orderItemsModelDao.OrderId,
                BookName = orderItemsModelDao.BookName,
                BookDescription = orderItemsModelDao.BookDescription,
                BookLanguage = orderItemsModelDao.BookLanguage,
                BookAuthor = orderItemsModelDao.BookAuthor,
                BookPrice = orderItemsModelDao.BookPrice,
                BookQuantity = orderItemsModelDao.BookQuantity,
                TotalPages = orderItemsModelDao.TotalPages,
                PreviousOwnership = orderItemsModelDao.PreviousOwnership,
                PublisherId = orderItemsModelDao.PublisherId,
            };
          if (_context.OrderItemsModel == null)
          {
              return Problem("Entity set 'BookwormContext.OrderItemsModel'  is null.");
          }
            _context.OrderItemsModel.Add(orderItemsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItemsModel", new { id = orderItemsModel.OrderItemsId }, orderItemsModel);
        }

        // DELETE: api/OrderItemsModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItemsModel(int id)
        {
            if (_context.OrderItemsModel == null)
            {
                return NotFound();
            }
            var orderItemsModel = await _context.OrderItemsModel.FindAsync(id);
            if (orderItemsModel == null)
            {
                return NotFound();
            }

            _context.OrderItemsModel.Remove(orderItemsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderItemsModelExists(int id)
        {
            return (_context.OrderItemsModel?.Any(e => e.OrderItemsId == id)).GetValueOrDefault();
        }
    }
}
