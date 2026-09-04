using LibraryApp.View_Model;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Tests;

public class RequestValidationTests
{
    [Fact]
    public void BookRequest_WithInvalidValues_ReturnsValidationErrors()
    {
        var request = new BookVm
        {
            Title = "",
            Price = -1,
            Description = "short",
            Genre = "",
            Rate = 8
        };

        var results = Validate(request);

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(BookVm.Title)));
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(BookVm.Price)));
        Assert.Contains(results, result => result.MemberNames.Contains(nameof(BookVm.Rate)));
    }

    [Fact]
    public void Registration_WithValidValues_PassesValidation()
    {
        var request = new RegisterVM
        {
            FirstName = "Portfolio",
            LastName = "User",
            UserName = "portfolio.user",
            Email = "portfolio@example.com",
            Phone = "+201000000000",
            Password = "Portfolio1!"
        };

        Assert.Empty(Validate(request));
    }

    private static List<ValidationResult> Validate(object value)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(value, new ValidationContext(value), results, validateAllProperties: true);
        return results;
    }
}
