using OutOfOffice.Entities;

namespace OutOfOffice.DTO.Responses;

public class EmployeeResponse
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public short OutOfOfficeBalance { get; set; }
    
    public string Subdivision { get; set; }

    public string Position { get; set; }

    public string Status { get; set; }
    
    public ICollection<string> Roles { get; set; } = null!;

    public int? PartnerId { get; set; }
}