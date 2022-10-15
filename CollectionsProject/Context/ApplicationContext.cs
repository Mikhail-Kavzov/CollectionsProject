using CollectionsProject.Models.CollectionModels;
using CollectionsProject.Models.ItemModels;
using CollectionsProject.Models.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollectionsProject.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<AddCollectionField> AddCollectionFields { get; set; } = null!;
        public DbSet<AddItemField> AddItemFields { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<UserComment> UserComments { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);
           builder.Entity<User>().HasMany(u => u.Comments).WithMany(c => c.Users).UsingEntity<UserComment>();
            builder.Entity<Tag>().HasAlternateKey(t=>t.TagName);
        }
    }
}

