using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.Services;
using Microsoft.AspNetCore.Identity;
using WeLearnOnine_Website.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ST_Welearn");
var connectionString1 = builder.Configuration.GetConnectionString("ST_Welearn1");

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection
builder.Services.AddDbContext<DerekmodeWeLearnSystemContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });


});

builder.Services.AddDbContext<WeLearnOnine_WebsiteContext>(options =>
{
    options.UseSqlServer(connectionString1);


});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WeLearnOnine_WebsiteContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 250 * 1024 * 1024; // 250 MB
});
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

//DI
builder.Services.AddTransient<Helper>();
builder.Services.AddTransient<IFavListRepository, FavListRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ILessonRepository, LessonRepository>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryCourseRepository, CategoryCourseRepository>();
builder.Services.AddTransient<ILevelRepository, LevelRepository>();
builder.Services.AddTransient<ISkillRepository, StaffRepository>();
builder.Services.AddTransient<ITypeRepository, TypeRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserCourseRatingRepository, UserCourseRatingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapAreaControllerRoute(
     name: "MyAreas",
     areaName: "Admin",
     pattern: "Admin/{controller}/{action=Index}/{id?}",
     defaults: new { controller = "Home", action = "Index" }
    );

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
