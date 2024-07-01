using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class PageRequest
{
    [Required]
    public string SortBy { get; set; } = "Id";
    [Required]
    public string SortDirection { get; set; } = "asc";
    [Required]
    public int Page { get; set; } = 1;
    [Required]
    public int PageSize { get; set; } = 10;
}