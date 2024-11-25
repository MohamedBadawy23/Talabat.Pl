using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Pl.DTO;

namespace Talabat.Pl.Helper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ForMember(P=>P.Department,O=>O.MapFrom(S=>S.Name));
        }
    }
}
