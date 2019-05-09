using Microsoft.EntityFrameworkCore;
using LoveDotNet.Models;

namespace LoveDotNet
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Article>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Paragraph>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Entity<Comment>().HasOne(a => a.User).WithMany(a => a.Comments).HasForeignKey(a => a.UserId);
            builder.Entity<Comment>().HasOne(a => a.Article).WithMany(a => a.Comments).HasForeignKey(a => a.ArticleId);
            builder.Entity<Article>().HasOne(a => a.Anthor).WithMany(a => a.Articles).HasForeignKey(a => a.UserId);
            builder.Entity<Paragraph>().HasOne(a => a.Article).WithMany(a => a.Paragraphs).HasForeignKey(a => a.ArticleId);
        }
    }
}
