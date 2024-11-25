using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specefication;
using Talabat.Pl.DTO;
using Talabat.Pl.Errors;
using Talabat.Repository.Data.Configuration;

namespace Talabat.Pl.Controllers
{
    
    public class EmployeeController :BaseController
    {
        private readonly IGenericRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IGenericRepository<Employee>employeeRepo,IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> GetEmployees()
        {
            var Spec =new EmployeeWithDepSpecs();
            var Employees = await _employeeRepo.GetAllAsyncSpecefication(Spec);
            var Mapped = _mapper.Map<IReadOnlyList<Employee>, IReadOnlyList<EmployeeDto>>(Employees);
            return Ok(Mapped);
        }
        [HttpGet("id")]
        public async Task<ActionResult<EmployeeDto>>GtById(int id)
        {
            var Spec = new EmployeeWithDepSpecs(id);
            var Result = await _employeeRepo.GetByIdAsyncSpecefication(Spec);
            if (Result is null) return NotFound(new ApiErrorsHandling(404));
            var MappedEmployee = _mapper.Map<Employee, EmployeeDto>(Result);
            return Ok(MappedEmployee);
        }
    }
}
