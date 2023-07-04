using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

	public DbSet<Comic> Comics { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options) {}


	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Comic>().Property(c => c.Title).IsRequired().HasMaxLength(50);
		builder.Entity<Comic>().Property(c => c.Issue).IsRequired();
		builder.Entity<Comic>().Property(c => c.Author).IsRequired().HasMaxLength(40);
		builder.Entity<Comic>().Property(c => c.Year).IsRequired();
		builder.Entity<Comic>().Property(c => c.Description).HasMaxLength(255).IsRequired(false);
		builder.Entity<Comic>().Property(c => c.Price).IsRequired();
		builder.Entity<Comic>().Property(c => c.Quantity).IsRequired();
		builder.Entity<Comic>().Property(c => c.IsRare).HasDefaultValue(false);
	}


}