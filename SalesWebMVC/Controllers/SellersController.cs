using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index() //tera q chamar o FindAll() do SellerService.. cria-se uma dependencia.
        {
            var list = _sellerService.FindAll(); //ira retornar uma lista de seller
            return View(list); //ira gerar um IActionResult contendo a lista (MVC acontecendo.. chamei o controlador 'index', controlador acessou o model sellerservice e encaminha os dados para a View()) ... Depois implementar na pagina index, um codigo de template para mostrar os vendedores (Views/Sellers/Index) 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //indica que essa ação é de Post, e não de GET
        [ValidateAntiForgeryToken]//notação para previnir que minha aplicação sofra ataques csrf(envia dados maliciosos aproveitando autentificação)
        public IActionResult Create(Seller seller) // no caso dessa operação, recebe o objeto que veio na requisição (para que receba o objeto e instancie a requisição, basta coloca-lo como parâmetro. O framework ja faz automáticamente)
        {
            //ação de inserir no banco de dados
            _sellerService.Insert(seller); //Método criado no serviço para inserir no banco de dados.

            // Redirecionar a requisição para ação index, que ira mostrar a tela principal do Crud de vendedores

            // return RedirectToAction("Index"); ou
            return RedirectToAction(nameof(Index)); // essa opção melhora a manutenção do sistema, pois se eu mudar o nome do string Index, não sera necessario mudar aqui
        }
    }
}

