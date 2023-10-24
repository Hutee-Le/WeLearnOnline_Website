using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<StaffSkill> StaffSkills { get; set; } = new List<StaffSkill>();
}
