using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index() //tera q chamar o FindAll() do SellerService.. cria-se uma dependencia.
        {
            var list = _sellerService.FindAll(); //ira retornar uma lista de seller
            return View(list); //ira gerar um IActionResult contendo a lista (MVC acontecendo.. chamei o controlador 'index', controlador acessou o model sellerservice e encaminha os dados para a View()) ... Depois implementar na pagina index, um codigo de template para mostrar os vendedores (Views/Sellers/Index) 
        }

        public IActionResult Create() 
        {
            // ---- att -----
            var departments = _departmentService.FindAll(); //carrega departamentos e buscar no banco de dados todos departamentos
            var viewModel = new SellerFormViewModel { Departments = departments }; //instanciar obj do viewmodel
            // --------------

            return View(viewModel);
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

        //abrir tela de confirmação de delete
        public IActionResult Delete(int? id) //interrogação é opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value); //value pois é um Nullable
            if (obj == null)
            {
                 return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value); //value pois é um Nullable
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }        
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                //Activity = System.Diagnostics . Current?(é opcional por conta do '?').Id - Se ele for nulo, é usado o operador de coalescencia nula '??' e digo que no lugar quero o obj HttpContex.TraceIdentifier
                // É um macete para pegar o ID interno da requisição.
            };
            return View(viewModel);
        }

    }
}

