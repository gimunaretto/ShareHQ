using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareHQ.Data;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class ItemController : Controller
    {
        private readonly IRepository<Item> _reposItem;
        private readonly IRepository<Categoria> _reposCategoria;

        public ItemController(IRepository<Item> reposItem, IRepository<Categoria> reposCategoria)
        {
            _reposItem = reposItem;
            _reposCategoria = reposCategoria;
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

            item.Categoria = _reposItem.GetById(item.CategoriaId).Categoria;
            _reposItem.Add(item);
            return RedirectToAction("Itens");
        }

        public IActionResult Itens()
        {
            var itensViewModel = new ItensViewModel()
            {
                Itens = _reposItem.GetAll()
            };

            return View(itensViewModel);
        }

        [HttpPost]
        public IActionResult Itens(ItensViewModel itensViewModel)
        {
            itensViewModel.Itens = _reposItem.GetAll();
            return View(itensViewModel);
        }

        public IActionResult EdicaoItem(int id)
        {
            MontaDropDownListCatergoria();

            var editando = _reposItem.GetById(id);

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

            item.Categoria = _reposCategoria.GetById(item.CategoriaId);
            _reposItem.Update(item);
            return RedirectToAction("Itens");
        }

        public IActionResult RemocaoItem(int id)
        {
            MontaDropDownListCatergoria();

            var removendo = _reposItem.GetById(id);

            if (removendo == null)
            {
                return RedirectToAction("Itens");
            }

            return View(removendo);
        }

        [HttpPost]
        public IActionResult RemocaoItem(Item item)
        {
            _reposItem.Remove(item);

            return RedirectToAction("Itens");
        }

        private void MontaDropDownListCatergoria()
        {
            var categorias = _reposCategoria.GetAll().Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            categorias.Insert(0, new SelectListItem { Value = "", Text = "Selecione Categoria" });
            ViewBag.Categorias = categorias;
        }
    }
}