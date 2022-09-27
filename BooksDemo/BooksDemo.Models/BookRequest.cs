using System.ComponentModel.DataAnnotations;

namespace BooksDemo.Models
{
    public class BookRequest
    {

        [Required, MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string AuthorName { get; set; }
    }
}
