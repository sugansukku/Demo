using Microsoft.EntityFrameworkCore;

namespace BooksDemo.Data.DataAccess
{
    public partial class BooksDBContext : DbContext
    {
        public const string BooksDBConnectionName = "BooksDB";
      
        public BooksDBContext(DbContextOptions<BooksDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookEntity> Books { get; set; } = null!;
    }
}
