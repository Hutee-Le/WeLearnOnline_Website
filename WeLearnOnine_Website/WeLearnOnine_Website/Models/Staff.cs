using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Staff
{
    public string StaffId { get; set; } = null!;

    public string StaffName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Desciption { get; set; }

    public string? Experience { get; set; }

    public string? AvatarUrl { get; set; }

    public string? PhoneNumber { get; set; }

    public string Role { get; set; } = null!;

    public double? Rating { get; set; }

    public string? Address { get; set; }

    public string? FacebookUrl { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<StaffSkill> StaffSkills { get; set; } = new List<StaffSkill>();
}
