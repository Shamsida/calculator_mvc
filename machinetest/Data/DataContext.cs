using machinetest.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace machinetest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<InputValue> Inputs { get; set; }
    }
}
