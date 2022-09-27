using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksDemo.Data.DataAccess
{
    [Table("BOOKS")]
    public partial class BookEntity
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }
        [Column("NAME")]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("AUTHORNAME")]
        [StringLength(100)]
        [Unicode(false)]
        public string Authorname { get; set; } = null!;
    }
}
