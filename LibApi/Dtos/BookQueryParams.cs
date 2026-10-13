using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos;

public class BookQueryParams
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 50)]
    public int Size { get; set; } = 5;

    public string? Genre { get; set; }
    public string? Author { get; set; }
    public string? Search { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }

    [RegularExpression("title|year|author|date")]
    public string SortBy { get; set; } = "title";

    [RegularExpression("asc|desc")]
    public string SortOrder { get; set; } = "asc";
}
