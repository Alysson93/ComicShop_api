using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

	public DbSet<Comic> Comics { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Coupon> Coupons { get; set; }
	public DbSet<Purchase> Purchases { get; set; }

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
	
		builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(50);
		builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(30);
		builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(50);
		builder.Entity<User>().Property(u => u.IsAdmin).IsRequired().HasDefaultValue(false);

		builder.Entity<Purchase>().Property(p => p.Coupon).IsRequired(false).HasMaxLength(6);

		builder.Entity<Coupon>().Property(c => c.Code).IsRequired().HasMaxLength(6);

	}


}