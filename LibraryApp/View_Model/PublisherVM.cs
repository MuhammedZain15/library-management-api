using System.ComponentModel.DataAnnotations;

namespace LibraryApp.View_Model;

public class PublisherVM
{
    [Required, StringLength(120, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
}
