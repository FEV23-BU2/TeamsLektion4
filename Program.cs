using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TeamsLektion4;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(
                "Host=localhost;Database=teamslektion4;Username=postgres;Password=password"
            );
        });

        var app = builder.Build();

        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}

/*

# Övning 1

public class Quote
{
    public int Id { get; set; }
    public string Quote { get; set; }
    public string Author { get; set; }
}

*/

/*

# Övning 2

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Author { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}
*/

/*

# Övning 3

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Author { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public string Author { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Quote quote = new Quote();
        quote.Author = dto.Author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(quote);
    }
}

*/

/*

# Övning 4

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Author { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public string Author { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Quote quote = new Quote();
        quote.Author = dto.Author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpGet]
    public List<Quote> GetAll()
    {
        return context.Quotes.ToList();
    }
}

*/

/*

# Övning 5

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Author { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public string Author { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Quote quote = new Quote();
        quote.Author = dto.Author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpGet]
    public List<Quote> GetAll()
    {
        return context.Quotes.ToList();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteQuote(int id)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Id == id).ExecuteDelete();

        // Metod 2
        Quote? quote = context.Quotes.Find(id);
        if (quote == null)
        {
            return NotFound();
        }

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }
}

*/

/*

# Övning 6

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Author { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public string Author { get; set; } = "";
}

public class RemoveByQuoteDto
{
    public string Quote { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Quote quote = new Quote();
        quote.Author = dto.Author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpGet]
    public List<Quote> GetAll()
    {
        return context.Quotes.ToList();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteQuote(int id)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Id == id).ExecuteDelete();

        // Metod 2
        Quote? quote = context.Quotes.Find(id);
        if (quote == null)
        {
            return NotFound();
        }

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpDelete("removeByQuote")]
    public IActionResult DeleteQuoteByQuote([FromBody] RemoveByQuoteDto dto)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Value == dto.Quote).ExecuteDelete();

        // Metod 2
        Quote quote = context.Quotes.Where(quote => quote.Value == dto.Quote).First();

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }
}

*/

/*

# Övning 8

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Author { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public string Author { get; set; } = "";
}

public class RemoveByQuoteDto
{
    public string Quote { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Quote quote = new Quote();
        quote.Author = dto.Author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpGet]
    public List<Quote> GetAll()
    {
        return context.Quotes.ToList();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteQuote(int id)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Id == id).ExecuteDelete();

        // Metod 2
        Quote? quote = context.Quotes.Find(id);
        if (quote == null)
        {
            return NotFound();
        }

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpDelete("removeByQuote")]
    public IActionResult DeleteQuoteByQuote([FromBody] RemoveByQuoteDto dto)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Value == dto.Quote).ExecuteDelete();

        // Metod 2
        Quote quote = context.Quotes.Where(quote => quote.Value == dto.Quote).First();

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpDelete("removeByAuthor")]
    public IActionResult DeleteQuoteByAuthor([FromQuery] string author)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Author == author).ExecuteDelete();

        // Metod 2
        List<Quote> quotes = context.Quotes.Where(quote => quote.Author == author).ToList();

        foreach (Quote quote in quotes)
        {
            context.Quotes.Remove(quote);
        }

        context.SaveChanges();

        return Ok(quotes);
    }
}

*/

/*

# Övning 8

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Author { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public string Author { get; set; } = "";
}

public class RemoveByQuoteDto
{
    public string Quote { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Quote quote = new Quote();
        quote.Author = dto.Author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpGet]
    public List<Quote> GetAll()
    {
        return context.Quotes.ToList();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteQuote(int id)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Id == id).ExecuteDelete();

        // Metod 2
        Quote? quote = context.Quotes.Find(id);
        if (quote == null)
        {
            return NotFound();
        }

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpDelete("removeByQuote")]
    public IActionResult DeleteQuoteByQuote([FromBody] RemoveByQuoteDto dto)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Value == dto.Quote).ExecuteDelete();

        // Metod 2
        Quote quote = context.Quotes.Where(quote => quote.Value == dto.Quote).First();

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpDelete("removeByAuthor")]
    public IActionResult DeleteQuoteByAuthor([FromQuery] string author)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Author == author).ExecuteDelete();

        // Metod 2
        List<Quote> quotes = context.Quotes.Where(quote => quote.Author == author).ToList();

        foreach (Quote quote in quotes)
        {
            context.Quotes.Remove(quote);
        }

        context.SaveChanges();

        return Ok(quotes);
    }

    [HttpGet("getByAuthor")]
    public List<Quote> GetQuotesByAuthor([FromQuery] string author)
    {
        return context.Quotes.Where(quote => quote.Author == author).ToList();
    }
}

*/

/*

# Övning 9

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public Author Author { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Author> Authors { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public string Author { get; set; } = "";
}

public class RemoveByQuoteDto
{
    public string Quote { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Quote quote = new Quote();
        quote.Author = dto.Author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpGet]
    public List<Quote> GetAll()
    {
        return context.Quotes.ToList();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteQuote(int id)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Id == id).ExecuteDelete();

        // Metod 2
        Quote? quote = context.Quotes.Find(id);
        if (quote == null)
        {
            return NotFound();
        }

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpDelete("removeByQuote")]
    public IActionResult DeleteQuoteByQuote([FromBody] RemoveByQuoteDto dto)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Value == dto.Quote).ExecuteDelete();

        // Metod 2
        Quote quote = context.Quotes.Where(quote => quote.Value == dto.Quote).First();

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(quote);
    }

    [HttpDelete("removeByAuthor")]
    public IActionResult DeleteQuoteByAuthor([FromQuery] string author)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Author == author).ExecuteDelete();

        // Metod 2
        List<Quote> quotes = context.Quotes.Where(quote => quote.Author == author).ToList();

        foreach (Quote quote in quotes)
        {
            context.Quotes.Remove(quote);
        }

        context.SaveChanges();

        return Ok(quotes);
    }

    [HttpGet("getByAuthor")]
    public List<Quote> GetQuotesByAuthor([FromQuery] string author)
    {
        return context.Quotes.Where(quote => quote.Author == author).ToList();
    }
}

*/

/*

# Övning 10 & 11

public class Quote
{
    public int Id { get; set; }
    public string Value { get; set; }
    public Author Author { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Quote> Quotes { get; set; } = new List<Quote>();
}

public class QuoteDto
{
    public int Id { get; set; }
    public string Quote { get; set; }
    public string AuthorName { get; set; }

    public QuoteDto(Quote quote)
    {
        this.Id = quote.Id;
        this.Quote = quote.Value;
        this.AuthorName = quote.Author.Name;
    }
}

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<QuoteDto> Quotes { get; set; } = new List<QuoteDto>();

    public AuthorDto(Author author)
    {
        this.Id = author.Id;
        this.Name = author.Name;
        this.Description = author.Description;
        this.Quotes = author.Quotes.Select(quote => new QuoteDto(quote)).ToList();
    }
}

public class ApplicationContext : DbContext
{
    // DbSet är en referens till 'Quote' tabellen som vi kan använda
    // för att hämta rader, skapa rader och så vidare.
    // Den kommer också att användas när vi skapar en eller flera migrations.
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Author> Authors { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}

public class CreateQuoteDto
{
    public string Quote { get; set; } = "";
    public int AuthorId { get; set; }
}

public class RemoveByQuoteDto
{
    public string Quote { get; set; } = "";
}

[ApiController]
[Route("api/quote")]
public class QuoteController : ControllerBase
{
    ApplicationContext context;

    public QuoteController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateQuote([FromBody] CreateQuoteDto dto)
    {
        Author? author = context.Authors.Find(dto.AuthorId);
        if (author == null)
        {
            return NotFound();
        }

        Quote quote = new Quote();
        quote.Author = author;
        quote.Value = dto.Quote;

        context.Quotes.Add(quote);
        context.SaveChanges();

        return Ok(new QuoteDto(quote));
    }

    [HttpGet]
    public List<QuoteDto> GetAll()
    {
        return context.Quotes.ToList().Select(quote => new QuoteDto(quote)).ToList();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteQuote(int id)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Id == id).ExecuteDelete();

        // Metod 2
        Quote? quote = context.Quotes.Find(id);
        if (quote == null)
        {
            return NotFound();
        }

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(new QuoteDto(quote));
    }

    [HttpDelete("removeByQuote")]
    public IActionResult DeleteQuoteByQuote([FromBody] RemoveByQuoteDto dto)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Value == dto.Quote).ExecuteDelete();

        // Metod 2
        Quote quote = context.Quotes.Where(quote => quote.Value == dto.Quote).First();

        context.Quotes.Remove(quote);
        context.SaveChanges();

        return Ok(new QuoteDto(quote));
    }

    [HttpDelete("removeByAuthor")]
    public IActionResult DeleteQuoteByAuthor([FromQuery] string author)
    {
        // Metod 1
        //context.Quotes.Where(quote => quote.Author.Name == author).ExecuteDelete();

        // Metod 2
        List<Quote> quotes = context
            .Quotes.Include(quote => quote.Author)
            .Where(quote => quote.Author.Name == author)
            .ToList();

        foreach (Quote quote in quotes)
        {
            context.Quotes.Remove(quote);
        }

        context.SaveChanges();

        return Ok(quotes.Select(quote => new QuoteDto(quote)).ToList());
    }

    [HttpGet("getByAuthor")]
    public List<QuoteDto> GetQuotesByAuthor([FromQuery] string author)
    {
        return context
            .Quotes.Include(quote => quote.Author)
            .Where(quote => quote.Author.Name == author)
            .ToList()
            .Select(quote => new QuoteDto(quote))
            .ToList();
    }
}

public class CreateAuthorDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}

[ApiController]
[Route("api/author")]
public class AuthorController : ControllerBase
{
    ApplicationContext context;

    public AuthorController(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public IActionResult CreateAuthor([FromBody] CreateAuthorDto dto)
    {
        Author author = new Author();
        author.Name = dto.Name;
        author.Description = dto.Description;

        context.Authors.Add(author);
        context.SaveChanges();

        return Ok(new AuthorDto(author));
    }
}

*/

/*

# Övning 12

Many to one

# Övning 13

One to many

*/
