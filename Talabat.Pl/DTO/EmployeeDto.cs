using Talabat.Core.Entities;

namespace Talabat.Pl.DTO
{
    public class EmployeeDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Age { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }
    }
}
