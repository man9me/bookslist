using bookslist.Data.Models;
using bookslist.Models;

using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookslist.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UsersToBooks>().HasOne(b => b.book).WithMany(ba => ba.UsersToBooks).HasForeignKey(bi => bi.bookId);
            builder.Entity<UsersToBooks>().HasOne(b => b.user).WithMany(ba => ba.UsersToBooks).HasForeignKey(bi => bi.userId);
            base.OnModelCreating(builder);
            
    }


        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<UsersToBooks> UsersToBooks { get; set; }
    }
}
