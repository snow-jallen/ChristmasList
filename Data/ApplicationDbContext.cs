using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ChristmasList.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<DesiredItem> DesiredItems { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class DesiredItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string ChildEmail { get; set; }
    }

    public class Suggestion
    {
        public int Id { get; set; }
        [DisplayName("Suggestion")]
        public string SuggestionText { get; set; }
        public int? ParentSuggestionId { get; set; }
        public Suggestion ParentSuggestion { get; set; }
        public DateTime AddedOn { get; set; }
        public List<Suggestion> ChildSuggestions { get; set; }
    }
}
