using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public int? LevelId { get; set; }

    public string StaffId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string? Required { get; set; }

    public string? ImageCourseUrl { get; set; }

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public string? PreviewUrl { get; set; }

    public bool Status { get; set; }

    public int? Count { get; set; }

    public double? Rating { get; set; }

    public string? Language { get; set; }

    public int? TimeTotal { get; set; }
    //public bool IsInFavorites { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual ICollection<CategoriesCourse> CategoriesCourses { get; set; } = new List<CategoriesCourse>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<FavList> FavLists { get; set; } = new List<FavList>();

    public virtual ICollection<HistoryPayment> HistoryPayments { get; set; } = new List<HistoryPayment>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Level? Level { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual ICollection<UserCourseRating> UserCourseRatings { get; set; } = new List<UserCourseRating>();
}
