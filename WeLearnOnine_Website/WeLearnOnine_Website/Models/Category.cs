using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Category
{
    public int CategoriesId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? ParentCategories { get; set; }

    public virtual ICollection<CategoriesCourse> CategoriesCourses { get; set; } = new List<CategoriesCourse>();
}
