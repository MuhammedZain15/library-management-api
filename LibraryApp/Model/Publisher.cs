namespace LibraryApp.Model
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Book> booksNV { get; set; } = [];
    }
}
