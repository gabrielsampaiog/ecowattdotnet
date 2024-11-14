using EcoWatt.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EcoWatt.Database
{
    public class AuthorizationDbContext(DbContextOptions options) : IdentityDbContext<AccessUser>(options)
    {
    }
}
