using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    public class EmployeeWithDepSpecs:BaseSpecefication<Employee>
    {
        
            public EmployeeWithDepSpecs()
            {
                Incudes.Add(P => P.Department);
            }
        public EmployeeWithDepSpecs(int id):base(P=>P.Id==id)
        {
            Incudes.Add(P => P.Department);
        }
        
    }
}
