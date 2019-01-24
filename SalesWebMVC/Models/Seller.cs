using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        //validaçao
        [Required(ErrorMessage = "{0} required")]//campo obrigatorio (opcional mensagem personalizada)
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]//tamanho minimo e maximo com mensagem de erro personalizada -- {0} Pega o nome do atributo, [1] pega o maxino {2} minimo 
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)] // acrescenta link toEmail
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        //muda o nome na pagina
        [Display(Name = "Birth Date")]
        // datatype altera o formato da data desejado
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; } //avisa o entity framework para garantir que esse id tem q existir e nao pode ser nulo
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department deparment)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = deparment;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final) //total de vendas
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount); //a partir da lista, filtro a lista (where) de vendas contendo apenas as vendas no intervalo de datas (sr (sales record) tal que sr. Date seja maior ou igual a data inciial ou sr . date seja menor ou igual a data final) depois mando calcular a soma de (sr. que leva em sr.Amount(eu quero a soma de vendas).
        }
    }
}
