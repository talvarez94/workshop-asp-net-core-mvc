using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class SellerService // a classe tem que ter uma dependencia para o context
    {
        private readonly SalesWebMVCContext _context; //readonly para previnir que a dependencia não possa ser alterada

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //retornar uma lista com todos os vendedores do banco de dados
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); //retorna do banco de dados _context .. ToList, isso acessa a fonte de dados relacionada a tabela de vendedores e converte em uma lista(operação sincrona, aplicação bloqueia esperando a operação terminar)
        }
    }
}
