using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? UserAsp { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<FavList> FavLists { get; set; } = new List<FavList>();

    public virtual ICollection<HistoryPayment> HistoryPayments { get; set; } = new List<HistoryPayment>();

    public virtual ICollection<UserCourseRating> UserCourseRatings { get; set; } = new List<UserCourseRating>();

    public virtual ICollection<UserLesson> UserLessons { get; set; } = new List<UserLesson>();
}
