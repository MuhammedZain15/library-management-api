using System.ComponentModel.DataAnnotations;

namespace LibraryApp.View_Model;

public class RegisterVM
{
    [Required, StringLength(80, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(80, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(40, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, Phone]
    public string Phone { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
