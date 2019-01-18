using System.Collections.Generic;

namespace SalesWebMVC.Models.ViewModels
{
    public class SellerFormViewModel // contem dados para o formulario de cadastro de vendedor
    {
        public Seller Seller { get; set; } //vendedor
        public ICollection<Department> Departments { get; set; }// lista de departamentos para criar caixinha para selecionar departamento do vendedor
    }
}
