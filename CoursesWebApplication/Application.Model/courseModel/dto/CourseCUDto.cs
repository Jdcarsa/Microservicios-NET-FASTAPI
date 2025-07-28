using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model.courseModel.dto
{
    public class CourseCUDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, 500)]
        public int Duration { get; set; }
        [Range(0.01, 10000.00)]
        public double Price { get; set; }

    }
}
