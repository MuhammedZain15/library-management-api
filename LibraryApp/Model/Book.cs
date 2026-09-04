using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string CoverURl { get; set; } = string.Empty;
        public string BookURl { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public DateTime? UpddatedDate { get; set; }
        [ForeignKey("PublisherNV")]
        public int? PublisherId { get; set; }
        public Publisher? PublisherNV { get; set; }

        public List<BookAuthor> bookAuthors { get; set; } = [];

    }
}
