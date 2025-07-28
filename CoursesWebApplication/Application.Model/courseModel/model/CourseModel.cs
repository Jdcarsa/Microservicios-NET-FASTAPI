using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Model.courseModel.model
{
    public class CourseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Duration { get; set; } 

        public double Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ImagePath { get; set; }

        public ICollection<PurchaseModel> Purchases { get; set; }
    }
}