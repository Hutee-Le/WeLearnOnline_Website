﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeLearnOnine_Website.Models;
using WeLearnOnine_Website.Repositories;


namespace WeLearnOnine_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private ICourseRepository _courseRepository;
        private ILessonRepository _lessonRepository;
        private ICategoryRepository _categoryRepository;
        private ILevelRepository _levelRepository;
        private ISkillRepository _staffRepository;

        public CourseController(ICourseRepository courseRepository, ICategoryRepository categoryRepository, ILevelRepository levelRepository, ISkillRepository staffRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _levelRepository = levelRepository;
            _staffRepository = staffRepository;
        }

        //View All Table Course
        public IActionResult Index(int? page)
        {
            int pageSize = 4; // Số lượng mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại (mặc định là 1 nếu không có giá trị

            var paginatedCourses = _courseRepository.GetAllCourses().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_courseRepository.GetAllCourses().Count() / pageSize);

            return View("Index", paginatedCourses);
        }

        // Create 
        [HttpPost]
        public IActionResult SaveCourse(Course course) 
        {
            _courseRepository.Add(course);
            if (course.CourseId == 0)
            {
                TempData["courseError"] = "Course not saved!";
            }
            else
            {
                TempData["courseSuccess"] = "Course successfully saved!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult CreateCourse()
        {
            var list1 = from c in _levelRepository.GetAllLevels()
                        select new SelectListItem()
                        {
                            Text = c.Name,
                            
                            Value = c.LevelId.ToString(),
                        };
            ViewBag.LevelId = list1.ToList();
            return View("CreateCourse", new Course());
        }

        [HttpPost]
        // Edit
        public IActionResult UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
            if (course.CourseId == 0)
            {
                TempData["courseError"] = "Course not saved!";
            }
            else
            {
                TempData["courseSuccess"] = "Course successfully saved!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditCourse(int id)
        {
            //var list1 = from c in _levelRepository.GetAllLevels()
            //            select new SelectListItem()
            //            {
            //                Text = c.Name,
            //                Value = c.LevelId.ToString(),
            //            };
            //ViewBag.LevelId = list1.ToList();

            var levellst = _levelRepository.GetAllLevels();
            var stafflst = _staffRepository.GetAllStaffs();
            ViewBag.LevelId = new SelectList(levellst, "LevelId", "Name");
            ViewBag.StaffId = new SelectList(stafflst, "StaffId", "StaffName");
            return View("EditCourse", _courseRepository.FindCourseByID(id));
        }
    
        // Delete
        public IActionResult Delete(int id) 
        { 
            _courseRepository.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
