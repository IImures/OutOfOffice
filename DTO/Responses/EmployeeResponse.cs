namespace OutOfOffice.DTO.Responses;

public class EmployeeResponse
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public short OutOffOfficeBalance { get; set; }
    
    public string Subdivision { get; set; }

    public string Position { get; set; }

    public string Status { get; set; }

    public List<string> Roles { get; set; } = new ();

    public int? PartnerId { get; set; }
}