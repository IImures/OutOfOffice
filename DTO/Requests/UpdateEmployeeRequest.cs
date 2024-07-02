using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class UpdateEmployeeRequest
{

    [MaxLength(100)] 
    public string? FullName { get; set; } = null!;
    [Range(0, short.MaxValue, ErrorMessage = "Value for must be greater or equal to 0.")]
    public short? OutOfOfficeBalance { get; set; }

    public int? SubdivisionId { get; set; }

    public int? PositionId { get; set; }

    public int? StatusId { get; set; }
     
    public int? PeoplePartnerId { get; set; }
    
    [MinLength(1)]
    public int[]? RolesId { get; set; }
}