using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model.courseModel.dto
{
    public class PurchaseCreateDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}
