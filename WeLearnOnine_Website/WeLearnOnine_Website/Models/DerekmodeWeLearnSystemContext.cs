using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeLearnOnine_Website.Models;

public partial class DerekmodeWeLearnSystemContext : DbContext
{
    public DerekmodeWeLearnSystemContext()
    {
    }

    public DerekmodeWeLearnSystemContext(DbContextOptions<DerekmodeWeLearnSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<CategoriesCourse> CategoriesCourses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<FavList> FavLists { get; set; }

    public virtual DbSet<HistoryPayment> HistoryPayments { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffSkill> StaffSkills { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCourseRating> UserCourseRatings { get; set; }

    public virtual DbSet<UserLesson> UserLessons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;uid=derekmode_WeLearn_System;password=123456;database=derekmode_WeLearn_System;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Bill__11F2FC4ADF4F3861");

            entity.ToTable("Bill");

            entity.Property(e => e.BillId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("BillID");
            entity.Property(e => e.CardHolderName)
                .HasMaxLength(50)
                .HasColumnName("Card_HolderName");
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("Expiration_date");
            entity.Property(e => e.HistoricalCost)
                .HasColumnType("money")
                .HasColumnName("Historical_cost");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.Promotion).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Bills)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Bill_User");
        });

        modelBuilder.Entity<BillDetail>(entity =>
        {
            entity.HasKey(e => e.BillDetailId).HasName("PK__BillDeta__793CAF7534987058");

            entity.ToTable("BillDetail");

            entity.Property(e => e.BillDetailId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("BillDetailID");
            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillDetail_Bill");

            entity.HasOne(d => d.Course).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillDetail_Course");
        });

        modelBuilder.Entity<CategoriesCourse>(entity =>
        {
            entity.HasKey(e => e.CatCourseId);

            entity.ToTable("Categories_Course");

            entity.Property(e => e.CatCourseId).HasColumnName("Cat_CourseID");
            entity.Property(e => e.CategoriesId).HasColumnName("CategoriesID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Topic).HasMaxLength(50);

            entity.HasOne(d => d.Categories).WithMany(p => p.CategoriesCourses)
                .HasForeignKey(d => d.CategoriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categories_Course_Categories");

            entity.HasOne(d => d.Course).WithMany(p => p.CategoriesCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categories_Course_Course");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoriesId);

            entity.Property(e => e.CategoriesId).HasColumnName("CategoriesID");
            entity.Property(e => e.CategoryName).HasMaxLength(70);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CmtId).HasName("PK_Comment_1");

            entity.ToTable("Comment");

            entity.Property(e => e.CmtId).HasColumnName("CmtID");
            entity.Property(e => e.ContentNote).HasColumnName("Content_Note");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.ReplyId).HasColumnName("ReplyID");
            entity.Property(e => e.StaffId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StaffID");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Course");

            entity.HasOne(d => d.Staff).WithMany(p => p.Comments)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Comment_Staff");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_Course_1");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.DiscountPrice).HasColumnType("money");
            entity.Property(e => e.ImageCourseUrl).HasColumnName("ImageCourseURL");
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.LevelId).HasColumnName("LevelID");
            entity.Property(e => e.PreviewUrl).HasColumnName("PreviewURL");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.StaffId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StaffID");
            entity.Property(e => e.Title).HasMaxLength(70);

            entity.HasOne(d => d.Level).WithMany(p => p.Courses)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK_Course_Level");

            entity.HasOne(d => d.Staff).WithMany(p => p.Courses)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Staff");
        });

        modelBuilder.Entity<FavList>(entity =>
        {
            entity.HasKey(e => e.FavId).HasName("PK_FavList_1");

            entity.ToTable("FavList");

            entity.Property(e => e.FavId).HasColumnName("FavID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.FavLists)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavList_Course");

            entity.HasOne(d => d.User).WithMany(p => p.FavLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_FavList_User");
        });

        modelBuilder.Entity<HistoryPayment>(entity =>
        {
            entity.ToTable("History_Payment");

            entity.Property(e => e.HistoryPaymentId).HasColumnName("History_PaymentID");
            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Bill).WithMany(p => p.HistoryPayments)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Payment_Bill");

            entity.HasOne(d => d.Course).WithMany(p => p.HistoryPayments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Payment_Course");

            entity.HasOne(d => d.User).WithMany(p => p.HistoryPayments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Payment_User");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.ToTable("Lesson");

            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ImageLessonUrl).HasColumnName("ImageLessonURL");
            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.Title).HasMaxLength(70);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.UrlVideo)
                .IsUnicode(false)
                .HasColumnName("URL_video");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_Course");

            entity.HasOne(d => d.Type).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_Type");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.ToTable("Level");

            entity.Property(e => e.LevelId).HasColumnName("LevelID");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skill");

            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.StaffId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StaffID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AvatarUrl)
                .IsUnicode(false)
                .HasColumnName("AvatarURL");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Experience).HasMaxLength(255);
            entity.Property(e => e.FacebookUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FacebookURL");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.StaffName).HasMaxLength(70);
        });

        modelBuilder.Entity<StaffSkill>(entity =>
        {
            entity.HasKey(e => e.SfSl);

            entity.ToTable("Staff_Skill");

            entity.Property(e => e.SfSl).HasColumnName("SF_SL");
            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.StaffId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StaffID");

            entity.HasOne(d => d.Skill).WithMany(p => p.StaffSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Skill_Skill");

            entity.HasOne(d => d.Staff).WithMany(p => p.StaffSkills)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Skill_Staff");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.Title).HasMaxLength(15);
            entity.Property(e => e.TypeDescription).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserCourseRating>(entity =>
        {
            entity.HasKey(e => e.Ucrid);

            entity.ToTable("User_Course_Rating");

            entity.Property(e => e.Ucrid).HasColumnName("UCRID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.UserCourseRatings)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Course_Rating_Course1");

            entity.HasOne(d => d.User).WithMany(p => p.UserCourseRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Course_Rating_User");
        });

        modelBuilder.Entity<UserLesson>(entity =>
        {
            entity.HasKey(e => e.Ulid);

            entity.ToTable("User_Lesson");

            entity.Property(e => e.Ulid).HasColumnName("ULID");
            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Lesson).WithMany(p => p.UserLessons)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Lesson_Lesson");

            entity.HasOne(d => d.User).WithMany(p => p.UserLessons)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Lesson_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
