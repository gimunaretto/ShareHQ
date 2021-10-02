using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class ItemController : Controller
    {
        private readonly IRepositorio _repositorio;

        public ItemController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Item()
        {
            MontaDropDownListCatergoria();
            return View();
        }

        [HttpPost]
        public IActionResult Item(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            item.Categoria = _repositorio.GetCategoriaById(item.CategoriaId);
            _repositorio.AddItem(item);
            return RedirectToAction("Itens");
        }

        public IActionResult Itens()
        {
            var itensViewModel = new ItensViewModel()
            {
                Itens = _repositorio.GetItens()
            };

            return View(itensViewModel);
        }

        [HttpPost]
        public IActionResult Itens(ItensViewModel itensViewModel)
        {
            itensViewModel.Itens = _repositorio.GetItens();
            return View(itensViewModel);
        }

        public IActionResult EdicaoItem(int id)
        {
            MontaDropDownListCatergoria();

            var editando = _repositorio.GetItemById(id);

            if (editando == null)
            {
                return RedirectToAction("Itens");
            }

            return View(editando);
        }

        [HttpPost]
        public IActionResult EdicaoItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            item.Categoria = _repositorio.GetCategoriaById(item.CategoriaId);
            _repositorio.UpdateItem(item);
            return RedirectToAction("Itens");
        }

        public IActionResult RemocaoItem(int id)
        {
            MontaDropDownListCatergoria();

            var removendo = _repositorio.GetItemById(id);

            if (removendo == null)
            {
                return RedirectToAction("Itens");
            }

            return View(removendo);
        }

        [HttpPost]
        public IActionResult RemocaoItem(Item item)
        {
            _repositorio.RemoveItem(item);

            return RedirectToAction("Itens");
        }

        private void MontaDropDownListCatergoria()
        {
            var categorias = _repositorio.GetCategorias().Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            categorias.Insert(0, new SelectListItem { Value = "", Text = "Selecione Categoria" });
            ViewBag.Categorias = categorias;
        }
    }
}