using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookslist.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UsersToBooks> UsersToBooks { get; set; }
    }
}
