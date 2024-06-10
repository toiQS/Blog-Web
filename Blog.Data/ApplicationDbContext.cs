using Blog_Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Blog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet properties
        public DbSet<Image> Images { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Remove the AspNet prefix from table names
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            // Seed data for Theme
            builder.Entity<Theme>().HasData(
                new Theme
                {
                    ThemeID = 1,
                    ThemeName = "C#",
                    Info = "Tổng hợp thông tin và các phiên bản cập nhật"
                },
                new Theme
                {
                    ThemeID = 2,
                    ThemeName = "Winform",
                    Info = "Tổng hợp thông tin và các phiên bản cập nhật"
                },
                new Theme
                {
                    ThemeID = 3,
                    ThemeName = "SSMS (Sql Server Manager Studio)",
                    Info = "Tổng hợp thông tin và các phiên bản cập nhật"
                });

            // Seed data for Poster
            builder.Entity<Poster>().HasData(
                new Poster
                {
                    PosterID = 1,
                    ThemeID = 1,
                    Title = "Lời nói đầu",
                    Intro = "C# (đọc là “C-sharp”) là một ngôn ngữ lập trình hiện đại, đa năng và hướng đối tượng...",
                    PosterContext = "Đặc Điểm Nổi Bật của C#...",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                },
                new Poster
                {
                    PosterID = 2,
                    ThemeID = 3,
                    Title = "Lời nói đầu về Sql",
                    Intro = "SQL (Structured Query Language) là ngôn ngữ truy vấn có cấu trúc...",
                    PosterContext = "Đặc Điểm Nổi Bật của SQL...",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                },
                new Poster
                {
                    PosterID = 3,
                    ThemeID = 2,
                    Title = "Lời nói đầu về Winform",
                    Intro = "Windows Forms, thường được gọi là WinForms, là một framework của Microsoft...",
                    PosterContext = "Đặc Điểm Nổi Bật của WinForms...",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                });

            // Seed data for Profile
            builder.Entity<Profile>().HasData(
                new Profile
                {
                    ProfileID = 1, // Ensure ProfileID is set if it is the primary key
                    FullName = "Nguyễn Quốc Siêu",
                    DateOfBirth = new DateTime(2002, 11, 12),
                    FaceBook = "facebook.com/toiQS",
                    Reddit = "reddit.com/user/toiQS",
                    Address = "District 4, Ho Chi Minh city",
                    Email = "nguyensieu1212002@gmail.com",
                    Phone = "0392828702"
                });

            // Seed data for Image
            builder.Entity<Image>().HasData(
                new Image
                {
                    ImageID = 1,
                    ImageName = "Noname",
                    ImageType = "Nothing",
                    ImageUrl = "Nothing",
                    PosterID = 1, // Valid PosterID
                    ProfileID = null // Not associated with a Profile
                },
                new Image
                {
                    ImageID = 2,
                    ImageName = "Noname",
                    ImageType = "Nothing",
                    ImageUrl = "Nothing",
                    PosterID = 2, // Valid PosterID
                    ProfileID = null // Not associated with a Profile
                },
                new Image
                {
                    ImageID = 3,
                    ImageName = "Noname",
                    ImageType = "Nothing",
                    ImageUrl = "Nothing",
                    PosterID = 3, // Valid PosterID
                    ProfileID = null // Not associated with a Profile
                },
                new Image
                {
                    ImageID = 4,
                    ImageName = "Noname",
                    ImageType = "Nothing",
                    ImageUrl = "Nothing",
                    PosterID = null, // Not associated with a Poster
                    ProfileID = 1 // Valid ProfileID
                });

            // Optionally set the navigation properties if needed
            builder.Entity<Poster>().HasOne(p => p.ImagePoster).WithOne(i => i.Poster).HasForeignKey<Image>(i => i.PosterID);
            builder.Entity<Profile>().HasOne(p => p.ProfileImage).WithOne(i => i.Profile).HasForeignKey<Image>(i => i.ProfileID);
        }

    }
}
