using Microsoft.AspNetCore.Mvc;
using ShareHQ.Data;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IRepository<Categoria> _reposCategoria;

        public CategoriaController(IRepository<Categoria> reposCategoria)
        {
            _reposCategoria = reposCategoria;
        }

        public IActionResult Categoria()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Categoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _reposCategoria.Add(categoria);
                return RedirectToAction("Categorias");
            }

            return View(categoria);
        }

        public IActionResult Categorias()
        {
            var viewModel = new CategoriasViewModel()
            {
                Categorias = _reposCategoria.GetAll(),
                Search = string.Empty
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Categorias(CategoriasViewModel viewModel)
        {
            viewModel.Categorias = _reposCategoria.GetAll();

            return View(viewModel);
        }

        public IActionResult CategoriaEdicao(int id)
        {
            var categoriaEditando = _reposCategoria.GetAll().FirstOrDefault(x => x.Id == id);
            if (categoriaEditando == null)
                return RedirectToAction("Categorias");

            return View(categoriaEditando);
        }

        [HttpPost]
        public IActionResult CategoriaEdicao(Categoria categoria)
        {
            _reposCategoria.Update(categoria);

            return RedirectToAction("Categorias");
        }

        public IActionResult CategoriaRemocao(int id)
        {
            var categoriaRemovendo = _reposCategoria.GetAll().FirstOrDefault(x => x.Id == id);
            if (categoriaRemovendo == null)
                return RedirectToAction("Categorias");

            return View(categoriaRemovendo);
        }

        [HttpPost]
        public IActionResult CategoriaRemocao(Categoria categoria)
        {
            _reposCategoria.Remove(categoria);

            return RedirectToAction("Categorias");
        }
    }
}