using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShareHQ.Models;
using System.Diagnostics;

namespace ShareHQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                return RedirectToAction("Index");
            }

            return View(item);
        }
        #endregion

        public IActionResult Emprestimo()
        {
            return View();
        }

        public IActionResult Item(ItemEmprestado itemEmprestado)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(itemEmprestado);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
