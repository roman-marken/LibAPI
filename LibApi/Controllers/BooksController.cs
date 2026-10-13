using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApi.Data;
using LibraryApi.Dtos;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public BooksController(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<BookDto>>> GetBooks([FromQuery] BookQueryParams queryParams)
    {
        var query = _context.Books.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(queryParams.Genre))
        {
            query = query.Where(b => b.Genre == queryParams.Genre);
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Author))
        {
            query = query.Where(b => b.Author.Contains(queryParams.Author));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            query = query.Where(b =>
                b.Title.Contains(queryParams.Search) ||
                b.Author.Contains(queryParams.Search));
        }

        if (queryParams.MinYear.HasValue)
        {
            query = query.Where(b => b.PublishedYear >= queryParams.MinYear.Value);
        }

        if (queryParams.MaxYear.HasValue)
        {
            query = query.Where(b => b.PublishedYear <= queryParams.MaxYear.Value);
        }

        query = SortBooks(query, queryParams.SortBy, queryParams.SortOrder);

        var totalItems = await query.CountAsync();

        var books = await query
            .Skip((queryParams.Page - 1) * queryParams.Size)
            .Take(queryParams.Size)
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(new PagedResult<BookDto>(books, totalItems, queryParams.Page, queryParams.Size));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> CreateBook(CreateBookRequest request)
    {
        var book = _mapper.Map<Book>(request);
        book.CreatedAt = DateTime.UtcNow;

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var dto = _mapper.Map<BookDto>(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, CreateBookRequest request)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _mapper.Map(request, book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static IQueryable<Book> SortBooks(IQueryable<Book> query, string sortBy, string sortOrder)
    {
        var desc = sortOrder == "desc";

        return sortBy switch
        {
            "year" => desc
                ? query.OrderByDescending(b => b.PublishedYear).ThenBy(b => b.Id)
                : query.OrderBy(b => b.PublishedYear).ThenBy(b => b.Id),
            "author" => desc
                ? query.OrderByDescending(b => b.Author).ThenBy(b => b.Id)
                : query.OrderBy(b => b.Author).ThenBy(b => b.Id),
            "date" => desc
                ? query.OrderByDescending(b => b.CreatedAt).ThenBy(b => b.Id)
                : query.OrderBy(b => b.CreatedAt).ThenBy(b => b.Id),
            _ => desc
                ? query.OrderByDescending(b => b.Title).ThenBy(b => b.Id)
                : query.OrderBy(b => b.Title).ThenBy(b => b.Id)
        };
    }
}
