using Microsoft.EntityFrameworkCore;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("System_Welearn");

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection
builder.Services.AddDbContext<StWelearnContext>(options =>
{
	options.UseSqlServer(connectionString);
});

//DI
builder.Services.AddTransient<IFavListRepository, FavListRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ILessonRepository, LessonRepository>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ILevelRepository, LevelRepository>();
builder.Services.AddTransient<IStaffRepository, StaffRepository>();

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
