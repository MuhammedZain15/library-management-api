namespace LibraryApp.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BookAuthor> bookAuthors { get; set; } = [];

    }
}
