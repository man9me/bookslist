using bookslist.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bookslist.Models
{
    
    public class UsersToBooks
    {

        [Key]
        public int Id { get; set; }
       
        public string userId { get; set; }       
        public ApplicationUser user { get; set; }
        
        public int bookId { get; set; }
        public Book book { get; set; }

        public UsersToBooks(string userId , int bookId)
        {
            this.userId = userId;
            this.bookId = bookId;
        }
    }
}
