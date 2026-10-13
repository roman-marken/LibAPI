using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos;

public class CreateBookRequest
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Genre { get; set; } = string.Empty;

    [Range(1, 3000)]
    public int PublishedYear { get; set; }

    [Range(0, 100000)]
    public decimal PurchasePrice { get; set; }

    public string InternalNotes { get; set; } = string.Empty;
    public string StorageLocation { get; set; } = string.Empty;
}
