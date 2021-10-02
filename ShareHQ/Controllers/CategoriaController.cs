using Microsoft.AspNetCore.Mvc;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IRepositorio _repositorio;

        public CategoriaController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
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
                _repositorio.AdicionaCategoria(categoria);
                return RedirectToAction("Categorias");
            }

            return View(categoria);
        }

        public IActionResult Categorias()
        {
            var viewModel = new CategoriasViewModel()
            {
                Categorias = _repositorio.Categorias,
                Search = string.Empty
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Categorias(CategoriasViewModel viewModel)
        {
            viewModel.Categorias = _repositorio.Categorias;

            return View(viewModel);
        }

        public IActionResult CategoriaEdicao(int id)
        {
            var categoriaEditando = _repositorio.Categorias.FirstOrDefault(x => x.Id == id);
            if (categoriaEditando == null)
                return RedirectToAction("Categorias");

            return View(categoriaEditando);
        }

        [HttpPost]
        public IActionResult CategoriaEdicao(Categoria categoria)
        {
            _repositorio.UpdateCategoria(categoria);

            return RedirectToAction("Categorias");
        }

        public IActionResult CategoriaRemocao(int id)
        {
            var categoriaRemovendo = _repositorio.Categorias.FirstOrDefault(x => x.Id == id);
            if (categoriaRemovendo == null)
                return RedirectToAction("Categorias");

            return View(categoriaRemovendo);
        }

        [HttpPost]
        public IActionResult CategoriaRemocao(Categoria categoria)
        {
            _repositorio.RemoveCategoria(categoria);

            return RedirectToAction("Categorias");
        }
    }
}