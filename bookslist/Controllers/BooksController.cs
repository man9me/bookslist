using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using System.Collections;

using System.Reflection;
using System.Text;
using System.Security.Claims;
using bookslist.Data;
using bookslist.Data.Models;

namespace bookslist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(  )
        {

            /*var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;*/

            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);//нужный айди блять
             var ID = claim.Value; 

            /*Console.WriteLine($"111~~~~~~~~~~~~~~~~~~~~~~~~~ {claims}  111 ");
            Console.WriteLine("sdqweqweqweqweqwewwwwwww");
            Console.WriteLine($"2222~~~~~~~~~~~~~~~~~~~~~~~~~ {claim2}   222 ");*/

            //var shit = ObjectDumper.Dump(claims);

            //Console.WriteLine(shit);

            return await _context.Books.ToListAsync();
        }

        /*// GET: api/Books/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);//нужный айди блять
            var ID = claim.Value;
            

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }*/
        /*
                // PUT: api/Books/5
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
                public async Task<IActionResult> PutBook(int id, Book book)
                {
                    if (id != book.Id)
                    {
                        return BadRequest();
                    }

                    _context.Entry(book).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BookExists(id))
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

                // POST: api/Books
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPost]
                public async Task<ActionResult<Book>> PostBook(Book book)
                {
                    _context.Books.Add(book);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetBook", new { id = book.Id }, book);
                }*/

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        //books
      /*  private async Task<bool> ToggleBook(string ID, int id)
        {
            var user = await _context.Users.FindAsync(ID);
            var books = user.Books;
            if (books != null && books != "")
            {
                var booksl =books.Split(',').ToList();
                foreach (var book in booksl)
                {

                }
            }
            

            return true;
        }*/
    }


    

}
