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
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bookwormapi.Controllers
{
    public class User
    {
        public string? userEmail { get; set; }
        public string? userPassword { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserModelsController : ControllerBase
    {
        private readonly BookwormContext _context;

        public UserModelsController(BookwormContext context)
        {
            _context = context;
        }

        // GET: api/UserModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUserModel()
        {
          if (_context.UserModel == null)
          {
              return NotFound();
          }
            return await _context.UserModel.ToListAsync();
        }

        [HttpPost("user/")]
        public async Task<ActionResult<UserModel>> isValidUser(User userDao)  
        {
            try
            {
                UserModel user = await _context.UserModel.Where(e => (e.UserEmail == userDao.userEmail && e.UserPassword == userDao.userPassword)).FirstAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem("OOPS! Incorrect Email/Password");

            }
            
            
        }

        // GET: api/UserModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(int id)
        {

            if (_context.UserModel == null)
          {
              return NotFound();
          }
            var userModel = await _context.UserModel.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return userModel;
        }

        [HttpPut("user/{id}")]
        public async Task<ActionResult<UserModel>> AccountUpdate(int id, [FromBody] AccountDetails details)
        {
            try
            {
                UserModel? publisher = await _context.UserModel.FindAsync(details.pid);
                UserModel? ownership = await _context.UserModel.FindAsync(details.oid);
                if (ownership != null)
                {
                    ownership.UserAccount += details.ownershipShare;
                    _context.Entry(ownership).State = EntityState.Modified;

                    publisher.UserAccount += details.publisherShare;
                    _context.Entry(publisher).State = EntityState.Modified;
                }
                else
                {
                    publisher.UserAccount += details.ownershipShare;
                    _context.Entry(publisher).State = EntityState.Modified;
                    

                }
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok();
            


        }

        // PUT: api/UserModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel(int id, [FromBody] UserModelDao UserModel)
        {
            UserModel userModel = await _context.UserModel.FindAsync(id);
            userModel.UserName = UserModel.UserName;
            userModel.UserPhone = UserModel.UserPhone;
            userModel.UserPassword = UserModel.UserPassword;
            userModel.UserAccount = UserModel.UserAccount;

            if (id != userModel.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        // POST: api/UserModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUserModel([FromBody] UserModelDao UserModel)
        {
            UserModel userModel = new UserModel()
            {
                UserName = UserModel.UserName,
                UserEmail = UserModel.UserEmail,
                UserPassword = UserModel.UserPassword,
                UserPhone = "",
                UserAccount = 0,
            };
          if (_context.UserModel == null)
          {
              return Problem("Entity set 'BookwormContext.UserModel'  is null.");
          }
          try
            {
                _context.UserModel.Add(userModel);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Email id already exists");
                return Problem("Email id already exists");
            }
            

            return CreatedAtAction("GetUserModel", new { id = userModel.UserId }, userModel);
        }

        // DELETE: api/UserModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel(int id)
        {
            if (_context.UserModel == null)
            {
                return NotFound();
            }
            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.UserModel.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserModelExists(int id)
        {
            return (_context.UserModel?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
