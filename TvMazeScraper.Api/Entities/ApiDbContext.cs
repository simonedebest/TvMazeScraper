using Microsoft.EntityFrameworkCore;

namespace TvMazeScraper.Api.Entities
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<ShowEntity> Shows { get; set; }
        public DbSet<CastMemberEntity> CastMembers { get; set; }
    }
}