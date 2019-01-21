using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

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

        //método para inserir um novo vendedor no banco de dados
        public void Insert(Seller obj)
        { 
            _context.Add(obj); //EntityFramework é bem simples. 
            _context.SaveChanges();//Confirma operação no banco de dados, depois criar a ação create com metódo POST no controlador.
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
            //include obj = Join, pegar informação de outra classe junto (microsoft.entityFrameworkCore)
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj) //atualizar um objeto do tipo Seller
        {
            //testa se o Id existe no banco. Any()existe algum registro no banco de dados 
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            //atualizar
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
