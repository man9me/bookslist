using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using System.Text.Json;
using Newtonsoft.Json.Linq;
using bookslist.Data;
using bookslist.Data.Models;
using bookslist.Models;
using bookslist.tools;
namespace bookslist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersToBooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public List<Book> books2;
        public List<Book> books4 = new List<Book>();
        public UsersToBooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // GET: api/UsersToBooks
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Book>>> GetUsersToBooks()
        {
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);//нужный айди блять
            var ID = claim.Value;


            Console.WriteLine("!!!!!!!!!!here!");
            //_context.UsersToBooks.ForEachAsync(x => Console.Write("{0}\t,{1}\t,{2}\t,{3}\t", x.book,x.bookId,x.user,x.userId));
            Console.WriteLine(ID);
            Console.WriteLine("~~~~~~~~~~~~");
            var UTB = _context.UsersToBooks.Where(p => p.user.Id == ID).ToList();
            var books = UTB.ConvertAll(new Converter<UsersToBooks, int>(x => x.bookId));

            //var books3 = UTB.ConvertAll(new Converter<UsersToBooks,Book >(x => _context.Books.Find(x.bookId)));

            foreach (var bookid in UTB)
            {
                var b = await _context.Books.FindAsync(bookid.bookId);
                b.UsersToBooks = null;
                Console.WriteLine($"111   {0}, {1}, {2} ", b.Id, b.Title, b.Description);
                books4.Add(b);
            }

           //UTB.ForEach(i => Console.WriteLine("{0}\t", i.bookId));
            //books.ForEach(i => Console.Write("{0}\t", i));
            Console.WriteLine("~~~~~~~~~~~~");
            //books3.ForEach(i => Console.WriteLine("{0}\t ,{1}  ,  {2}  ", i.Title,i.Id,i.Description));
            /*
            var json = JsonSerializer.Serialize(books4);
            JObject parsed = JObject.Parse(json);

            foreach (var pair in parsed)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }*/

            return books4;

            /*
            foreach (var book in books)
            {
               var b = await _context.Books.FindAsync(book);
               books2.Add(b);
            }*/
        }

        // GET: api/UsersToBooks/5
    /*    [HttpGet("{id}")]
        public async Task<ActionResult<UsersToBooks>> GetUsersToBooks(int id)
        {
            var usersToBooks = await _context.UsersToBooks.FindAsync(id);

            if (usersToBooks == null)
            {
                return NotFound();
            }

            return usersToBooks;
        }*/

        // PUT: api/UsersToBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersToBooks(int id, UsersToBooks usersToBooks)
        {
            if (id != usersToBooks.Id)
            {
                return BadRequest();
            }

            _context.Entry(usersToBooks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersToBooksExists(id))
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
        
        // POST: api/UsersToBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task PostUsersToBooks(int[] mybooks)
        {

            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);//нужный айди блять
            var ID = claim.Value;
            System.Console.WriteLine("!!! priem eba");
            var select = mybooks.Select(x => new UsersToBooks(ID, x));
            var quer = _context.UsersToBooks.Where(x => (x.userId == ID));
          

           
            _context.UsersToBooks.RemoveRange(quer);
            _context.UsersToBooks.AddRange(select);
            

          /*  foreach (var book in mybooks)
            {
                var bookl = _context.UsersToBooks.FirstOrDefault(p => p.user.Id == ID & p.bookId == book);
                if (bookl!=null)
                {                  
                    UsersToBooks b = new UsersToBooks();
                    b.bookId=book;
                    b.userId=ID;
                    _context.UsersToBooks.Add(b);
                }
            };*/
            await _context.SaveChangesAsync();
            // _context.UsersToBooks.Add(usersToBooks);
            // await _context.SaveChangesAsync();

            // return CreatedAtAction("GetUsersToBooks", new { id = usersToBooks.Id }, usersToBooks);
        }

        // DELETE: api/UsersToBooks/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersToBooks(int id)
        {
            var usersToBooks = await _context.UsersToBooks.FindAsync(id);
            if (usersToBooks == null)
            {
                return NotFound();
            }

            _context.UsersToBooks.Remove(usersToBooks);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool UsersToBooksExists(int id)
        {
            return _context.UsersToBooks.Any(e => e.Id == id);
        }
    }
}
