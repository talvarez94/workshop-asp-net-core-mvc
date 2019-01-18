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
    }
}

