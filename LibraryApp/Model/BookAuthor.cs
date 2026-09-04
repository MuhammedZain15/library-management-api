using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Model
{
    public class BookAuthor
    {
        public int Id { get; set; }
        [ForeignKey("book")]
        public int BookId { get; set; }
        public Book bookNV { get; set; } = null!;
        [ForeignKey("author")]
        public int AuthorId { get; set; }
        public Author authorNV { get; set; } = null!;

    }
}
