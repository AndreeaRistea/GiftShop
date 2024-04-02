using GiftShopOnline.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GiftShopOnline.Data;

public class UnitOfWork : DbContext
{
    public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
   // public DbSet<Cart> Carts { get; set; }  
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Wishlist> Wishlist { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseSerialColumns();

        modelBuilder.Entity<Wishlist>()
                 .HasOne(e => e.User)
                 .WithOne(e => e.Wishlist)
                 .HasForeignKey<User>();

        //modelBuilder.Entity<Cart>()
        //    .HasOne(e => e.User)
        //    .WithOne(e => e.Cart)
        //    .HasForeignKey<User>();

        modelBuilder.Entity<CartItem>()
            .HasOne(e => e.Product)
            .WithOne(e => e.CartItem)
            .HasForeignKey<CartItem>(e => e.ProductId);
    }
}

