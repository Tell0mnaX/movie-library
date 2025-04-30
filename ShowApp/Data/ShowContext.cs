using Microsoft.EntityFrameworkCore;
using ShowApp.Models;

public class ShowContext : DbContext
{
    public ShowContext(DbContextOptions<ShowContext> option) : base (option){}

    public DbSet<Show> Shows {get; set;}
    public DbSet<User> Users { get; set; }

}