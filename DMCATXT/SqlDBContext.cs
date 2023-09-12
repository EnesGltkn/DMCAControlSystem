using DMCATXT.Models;
using Microsoft.EntityFrameworkCore;

namespace DMCATXT
{
    public class SqlDBContext : DbContext
    {
        public SqlDBContext(DbContextOptions<SqlDBContext> options)
          : base(options)
        {

        }
        public DbSet<UploadInfo> UploadInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UploadInfo>()
                 .HasKey(tc => new { tc.ID });
        }

    }
}
