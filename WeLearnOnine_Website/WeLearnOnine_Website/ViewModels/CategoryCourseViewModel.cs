using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class CategoryCourseViewModel
	{
        public List<Course> Courses { get; set; }
        public List<Category> Categories { get; set; }

        public List<CategoriesCourse> CateCourse { get; set; }
        public int? SelectedCategoryId { get; set; }

        public int CatCourseId { get; set; }

        public int CategoriesId { get; set; }

        public string? CategoryName { get; set; }

        public int? ParentCategories { get; set; }

        /*--------------- LINQ -------------------
            SELECT TOP (1000) [Cat_CourseID]
                  ,[CategoriesID]
                  ,[CourseID]
                  ,[Topic]
              FROM [derekmode_WeLearn_System].[dbo].[Categories_Course]

            SELECT 
                [dbo].[Course].Title 
            FROM 
                Course
	            WHERE Course.CourseID = 5

            SELECT 
                [dbo].[Categories].CategoryName AS nameCategory,
                [dbo].[Course].Title AS nameCourse
            FROM 
                Course
            INNER JOIN 
                [dbo].[Categories_Course] ON [dbo].[Course].CourseID = [dbo].[Categories_Course].CourseID
            INNER JOIN 
                [dbo].[Categories] ON [dbo].[Categories_Course].CategoriesID = [dbo].[Categories].CategoriesID 
         */
    }
}
