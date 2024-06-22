using Microsoft.EntityFrameworkCore;

namespace OutOfOffice.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    
}