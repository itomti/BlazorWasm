using BlazorWasm.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Server.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Unit> Units { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserUnit> UserUnits { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
  }
}
