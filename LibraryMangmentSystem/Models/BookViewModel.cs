using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMangmentSystem.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        public bool IsAvailable { get; set; }
        public String CategoryName { get; set; }

    }
}