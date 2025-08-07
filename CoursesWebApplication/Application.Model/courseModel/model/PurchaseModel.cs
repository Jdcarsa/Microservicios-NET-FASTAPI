using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.Model.courseModel.model
{
    public class PurchaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [JsonIgnore]
        [ForeignKey("CourseId")]
        public CourseModel Course { get; set; }

        [Required]
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;

        public double Price { get; set; }
    }
}