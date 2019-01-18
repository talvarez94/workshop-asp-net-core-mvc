using System.Linq;
using System.Collections.Generic;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _contex;

        public DepartmentService(SalesWebMVCContext context)
        {
            _contex = context;
        }

        //metodos para retornar todos os departamentos
        public List<Department> FindAll()
        {
            return _contex.Department.OrderBy(x => x.Name).ToList(); //retorna lista ordenada por nome
        }
    }
}
