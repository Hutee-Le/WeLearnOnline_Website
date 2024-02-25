using System;
using System.Collections.Generic;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.ViewModels
{
    public class BillDetailViewModel
    {
        public Guid BillDetailId { get; set; }
        public int CourseId { get; set; } 
        // Các thuộc tính khác của BillDetail...
        public string Title { get; set; }

        public string ImageCourseUrl { get; set; }
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }
        // Các thuộc tính khác của Course...
    }
}
