using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model.courseModel.dto
{
    public class PurchaseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime PurchasedAt { get; set; }
        public double Price { get; set; }
    }
}
