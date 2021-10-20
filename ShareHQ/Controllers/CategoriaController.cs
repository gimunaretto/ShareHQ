using Microsoft.AspNetCore.Mvc;
using ShareHQ.Data;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IRepositoryCategoria _repositorio;

        public CategoriaController(IRepositoryCategoria repositorio)
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
                _repositorio.Add(categoria);
                return RedirectToAction("Categorias");
            }

            return View(categoria);
        }

        public IActionResult Categorias()
        {
            var viewModel = new CategoriasViewModel()
            {
                Categorias = _repositorio.GetAll(),
                Search = string.Empty
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Categorias(CategoriasViewModel viewModel)
        {
            viewModel.Categorias = _repositorio.GetAll();

            return View(viewModel);
        }

        public IActionResult CategoriaEdicao(int id)
        {
            var categoriaEditando = _repositorio.GetAll().FirstOrDefault(x => x.Id == id);
            if (categoriaEditando == null)
                return RedirectToAction("Categorias");

            return View(categoriaEditando);
        }

        [HttpPost]
        public IActionResult CategoriaEdicao(Categoria categoria)
        {
            _repositorio.Update(categoria);

            return RedirectToAction("Categorias");
        }

        public IActionResult CategoriaRemocao(int id)
        {
            var categoriaRemovendo = _repositorio.GetAll().FirstOrDefault(x => x.Id == id);
            if (categoriaRemovendo == null)
                return RedirectToAction("Categorias");

            return View(categoriaRemovendo);
        }

        [HttpPost]
        public IActionResult CategoriaRemocao(Categoria categoria)
        {
            _repositorio.Remove(categoria);

            return RedirectToAction("Categorias");
        }
    }
}