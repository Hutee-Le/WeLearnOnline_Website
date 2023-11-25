using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;
using WeLearnOnine_Website.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ST_Welearn");

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection
builder.Services.AddDbContext<DerekmodeWeLearnSystemContext>(options =>
{
	options.UseSqlServer(connectionString);
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 250 * 1024 * 1024; // 250 MB
});
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

//DI
builder.Services.AddTransient<IFavListRepository, FavListRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ILessonRepository, LessonRepository>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ILevelRepository, LevelRepository>();
builder.Services.AddTransient<ISkillRepository, StaffRepository>();
builder.Services.AddTransient<ITypeRepository, TypeRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

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

app.Run();
