using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareHQ.Data;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System;
using System.Globalization;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class ItemEmprestadoController : Controller
    {
        private readonly IRepository<ItemEmprestado> _reposItemEmprestado;
        private readonly IRepository<Item> _reposItem;
        private readonly IRepository<Usuario> _reposUsuario;

        public ItemEmprestadoController(
            IRepository<ItemEmprestado> reposItemEmprestado, 
            IRepository<Item> reposItem,
            IRepository<Usuario> reposUsuario)
        {
            _reposItemEmprestado = reposItemEmprestado;
            _reposItem = reposItem;
            _reposUsuario = reposUsuario;
        }

        public IActionResult Emprestimo()
        {
            var itens = _reposItem.GetAll().Where(x => x.Disponivel).Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();
            itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
            ViewBag.Itens = itens;

            var usuarios = _reposUsuario.GetAll().Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
            ViewBag.Usuarios = usuarios;

            return View();
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var culture = new CultureInfo("pt-BR", false);

            var events = _reposItemEmprestado.GetAll().Select(e => new
            {
                id    = e.Id,
                title = _reposItem.GetById(e.ItemId).Titulo,
                text  = _reposUsuario.GetById(e.UsuarioId).Nome,
                start = e.DataEmprestimo.ToString("yyyy-MM-dd HH:mm:ss"),
                end   = e.DataEmprestimo.AddDays(e.PrazoDeDevolucao).ToString("yyyy-MM-dd HH:mm:ss"),
            }).ToList();

            return new JsonResult(events);
        }

        [HttpPost]
        public IActionResult Emprestimo(ItemEmprestado itemEmprestado)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Emprestimo");
            }

            itemEmprestado.StatusDevolucao = (int)ItemEmprestado.DevolucaoStatus.NoPrazo;
            itemEmprestado.StatusEmprestimo = (int)ItemEmprestado.EmprestimoStatus.EmEmprestimo;
            itemEmprestado.Usuario = _reposUsuario.GetById(itemEmprestado.UsuarioId);
            itemEmprestado.Item = _reposItem.GetById(itemEmprestado.ItemId);
            
            _reposItemEmprestado.Add(itemEmprestado);

            itemEmprestado.Item.Disponivel = false;
            var item = itemEmprestado.Item;
            _reposItem.Update(item);

            return RedirectToAction("Emprestimo");
        }

        public IActionResult ListaItens()
        {
            var itensEmprestadosViewModel = new ItensEmprestadosViewModel()
            {
                Itens = _reposItemEmprestado.GetAll()
            };

            foreach (var i in itensEmprestadosViewModel.Itens)
            {
                i.Item = _reposItem.GetById(i.ItemId);
                i.Usuario = _reposUsuario.GetById(i.UsuarioId);
            }

            return View(itensEmprestadosViewModel);
        }

        [HttpPost]
        public IActionResult ListaItens(ItensEmprestadosViewModel itensEmprestadosViewModel)
        {
            itensEmprestadosViewModel.Itens = _reposItemEmprestado.GetAll();
            return View(itensEmprestadosViewModel);
        }

        public IActionResult EdicaoItemEmprestado(int id)
        {
            var itens = _reposItem.GetAll().Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();

            itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
            ViewBag.Itens = itens;

            var usuarios = _reposUsuario.GetAll().Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
            ViewBag.Usuarios = usuarios;

            var editando = _reposItemEmprestado.GetById(id);

            if (editando == null)
            {
                return RedirectToAction("ListaItens");
            }

            return View(editando);
        }

        [HttpPost]
        public IActionResult EdicaoItemEmprestado(ItemEmprestado itemEmprestado)
        {
            if (!ModelState.IsValid)
            {
                var itens = _reposItem.GetAll().Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();

                itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
                ViewBag.Itens = itens;

                var usuarios = _reposUsuario.GetAll().Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
                ViewBag.Usuarios = usuarios;

                return View(itemEmprestado);
            }

            itemEmprestado.Item = _reposItem.GetById(itemEmprestado.ItemId);
            var deadLine = itemEmprestado.DataEmprestimo.AddDays(itemEmprestado.PrazoDeDevolucao);

            if (itemEmprestado.DataDevolucao.HasValue)
            {
                if (itemEmprestado.DataDevolucao > deadLine)
                {
                    itemEmprestado.Item.Disponivel = true;
                    itemEmprestado.StatusDevolucao = (int)ItemEmprestado.EmprestimoStatus.Devolvido;
                }
            }
            else
            {
                itemEmprestado.StatusEmprestimo = (int)ItemEmprestado.EmprestimoStatus.EmEmprestimo;

                if (DateTime.Now > deadLine)
                {
                    itemEmprestado.StatusDevolucao = (int)ItemEmprestado.DevolucaoStatus.Atrasado;
                }
                else
                {
                    itemEmprestado.StatusDevolucao = (int)ItemEmprestado.DevolucaoStatus.NoPrazo;
                }
            }

            itemEmprestado.Item = _reposItem.GetById(itemEmprestado.ItemId);
            itemEmprestado.Usuario = _reposUsuario.GetById(itemEmprestado.UsuarioId);
            _reposItemEmprestado.Update(itemEmprestado);
            return RedirectToAction("ListaItens");
        }

        public IActionResult RemocaoItemEmprestado(int id)
        {
            var itens = _reposItem.GetAll().Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();

            itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
            ViewBag.Itens = itens;

            var usuarios = _reposUsuario.GetAll().Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
            ViewBag.Usuarios = usuarios;

            var removendo = _reposItemEmprestado.GetById(id);

            if (removendo == null)
            {
                return RedirectToAction("ListaItens");
            }

            return View(removendo);
        }

        [HttpPost]
        public IActionResult RemocaoItemEmprestado(ItemEmprestado itemEmprestado)
        {
            itemEmprestado = _reposItemEmprestado.GetById(itemEmprestado.Id);
            var itemVerificado = _reposItem.GetById(itemEmprestado.ItemId);
            itemVerificado.Disponivel = true;
            var item = itemVerificado;
            
            _reposItem.Update(item);
            _reposItemEmprestado.Remove(itemEmprestado);

            return RedirectToAction("ListaItens");
        }
    }
}