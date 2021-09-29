using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShareHQ.Models;
using System.Diagnostics;

namespace ShareHQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorio _repositorio;

        public HomeController(ILogger<HomeController> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #region [Usuário]
        public IActionResult Usuario()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Usuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(usuario);
        }
        #endregion

        #region [Categoria]
        public IActionResult Categoria()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Categoria(Categoria categoria)
        {
              
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(categoria);
        }
        #endregion

        #region [Cadastro de Itens]
        public IActionResult Item()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Item(Item item)
        {   
            if (ModelState.IsValid)
            {
                item.Categoria = _repositorio.GetCategoriaById(item.CategoriaId);

                return RedirectToAction("Index");
            }

            return View(item);
        }
        #endregion

        #region [Emprestimo de Item]
        public IActionResult Emprestimo()
        {
            return View();
        }

        public IActionResult Emprestimo(ItemEmprestado itemEmprestado)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(itemEmprestado);
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
