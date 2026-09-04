using LibraryApp.Model;

using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data
{
    public class AppDbInit
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
       new Book()
       {
           Title = "Clean Code",
           Price = 300,
           Description = "A handbook of agile software craftsmanship.",
           Author = "Robert C. Martin",
           IsRead = true,
           Genre = "Programming",
           CoverURl = "https://example.com/clean-code.jpg",
           BookURl = "https://example.com/clean-code.pdf",
           AddedDate = DateTime.Now,

       },
       new Book()
       {
           Title = "The Pragmatic Programmer",
           Price = 250,
           Description = "Journey to mastery in software development.",
           Author = "Andrew Hunt",
           IsRead = false,
           Genre = "Programming",
           CoverURl = "https://example.com/pragmatic.jpg",
           BookURl = "https://example.com/pragmatic.pdf",
           AddedDate = DateTime.Now,

       },
       new Book()
       {
           Title = "Atomic Habits",
           Price = 200,
           Description = "An easy & proven way to build good habits.",
           Author = "James Clear",
           IsRead = true,
           Genre = "Self Development",
           CoverURl = "https://example.com/atomic.jpg",
           BookURl = "https://example.com/atomic.pdf",
           AddedDate = DateTime.Now,

       }
   );

                    await context.SaveChangesAsync();
                }
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(
                        new Author()
                        {
                            Name = "mohmed"
                        },
                        new Author()
                        {
                            Name = "ahmed"
                        },
                        new Author()
                        {
                            Name = "ali"
                        },
                        new Author()
                        {
                            Name = "essam"
                        },
                        new Author()
                        {
                            Name = "malak"
                        }
                        );
                    await context.SaveChangesAsync();
                }
                if (!context.Publishers.Any())
                {
                    context.Publishers.AddRange(
                        new Publisher()
                        {
                            Name = "hassan ebrahim"
                        },
                        new Publisher()
                        {
                            Name = "emam ashour"
                        },
                        new Publisher()
                        {
                            Name = "tarek hamed"
                        },
                        new Publisher()
                        {
                            Name = "abdala mohamed"
                        },
                        new Publisher()
                        {
                            Name = "reham mohamed"
                        }
                        );
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
