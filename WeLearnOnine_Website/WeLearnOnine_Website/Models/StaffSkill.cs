using System;
using System.Collections.Generic;

namespace WeLearnOnine_Website.Models;

public partial class StaffSkill
{
    public int SfSl { get; set; }

    public string StaffId { get; set; } = null!;

    public int SkillId { get; set; }

    public virtual Skill Skill { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
