using System.ComponentModel.DataAnnotations;

namespace LibraryApp.View_Model;

public class BookVm
{
    [Required, StringLength(200, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [Range(0, 1_000_000)]
    public int Price { get; set; }

    [Required, StringLength(3000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Author { get; set; }

    public bool IsRead { get; set; }
    public DateTime? ReadDate { get; set; }

    [Range(1, 5)]
    public int? Rate { get; set; }

    [Required, StringLength(80)]
    public string Genre { get; set; } = string.Empty;

    [Url]
    public string? CoverUrl { get; set; }

    [Url]
    public string? BookUrl { get; set; }

    public int? PublisherId { get; set; }
    public List<int> AuthorsId { get; set; } = [];
}
