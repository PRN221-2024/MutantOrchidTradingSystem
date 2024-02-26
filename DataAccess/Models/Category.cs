using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseCategoryNavigation = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? CategoryNavigation { get; set; }
        public virtual ICollection<Category> InverseCategoryNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
