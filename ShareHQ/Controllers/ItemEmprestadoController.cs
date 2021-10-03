using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System;
using System.Globalization;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class ItemEmprestadoController : Controller
    {
        private readonly IRepositorio _repositorio;

        public ItemEmprestadoController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Emprestimo()
        {
            var itens = _repositorio.GetItens().Where(x => x.Disponivel).Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();
            itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
            ViewBag.Itens = itens;

            var usuarios = _repositorio.Usuarios.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
            ViewBag.Usuarios = usuarios;

            return View();
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var culture = new CultureInfo("pt-BR", false);

            var events = _repositorio.GetEmprestado().Select(e => new
            {
                id = e.Id,
                title = _repositorio.GetItemById(e.ItemId).Titulo,
                text = _repositorio.Usuarios.FirstOrDefault(x => x.Id == e.UsuarioId).Nome,
                start = e.DataEmprestimo.ToString("yyyy-MM-dd HH:mm:ss"),
                end = e.DataEmprestimo.AddDays(e.PrazoDeDevolucao).ToString("yyyy-MM-dd HH:mm:ss"),
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
            itemEmprestado.Usuario = _repositorio.Usuarios.FirstOrDefault(x => x.Id == itemEmprestado.UsuarioId);
            itemEmprestado.Item = _repositorio.GetItemById(itemEmprestado.ItemId);
            _repositorio.Add(itemEmprestado);

            itemEmprestado.Item.Disponivel = false;
            var item = itemEmprestado.Item;
            _repositorio.UpdateItem(item);

            return RedirectToAction("Emprestimo");
        }

        public IActionResult ListaItens()
        {
            var itensEmprestadosViewModel = new ItensEmprestadosViewModel()
            {
                Itens = _repositorio.GetEmprestado()
            };
            foreach (var i in itensEmprestadosViewModel.Itens)
            {
                i.Item = _repositorio.GetItemById(i.ItemId);
                i.Usuario = _repositorio.Usuarios.FirstOrDefault(x => x.Id == i.UsuarioId);
            }

            return View(itensEmprestadosViewModel);
        }

        [HttpPost]
        public IActionResult ListaItens(ItensEmprestadosViewModel itensEmprestadosViewModel)
        {
            itensEmprestadosViewModel.Itens = _repositorio.GetEmprestado();
            return View(itensEmprestadosViewModel);
        }

        public IActionResult EdicaoItemEmprestado(int id)
        {
            var itens = _repositorio.GetItens().Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();

            itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
            ViewBag.Itens = itens;

            var usuarios = _repositorio.Usuarios.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
            ViewBag.Usuarios = usuarios;

            var editando = _repositorio.GetEmprestadoById(id);

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
                var itens = _repositorio.GetItens().Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();

                itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
                ViewBag.Itens = itens;

                var usuarios = _repositorio.Usuarios.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
                ViewBag.Usuarios = usuarios;

                return View(itemEmprestado);
            }

            itemEmprestado.Item = _repositorio.GetItemById(itemEmprestado.ItemId);
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

            itemEmprestado.Item = _repositorio.GetItemById(itemEmprestado.ItemId);
            itemEmprestado.Usuario = _repositorio.Usuarios.FirstOrDefault(x => x.Id == itemEmprestado.UsuarioId);
            _repositorio.Update(itemEmprestado);
            return RedirectToAction("ListaItens");
        }

        public IActionResult RemocaoItemEmprestado(int id)
        {
            var itens = _repositorio.GetItens().Select(x => new SelectListItem() { Text = x.Titulo, Value = x.Id.ToString() }).ToList();

            itens.Insert(0, new SelectListItem { Value = "", Text = "Selecione o item" });
            ViewBag.Itens = itens;

            var usuarios = _repositorio.Usuarios.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            usuarios.Insert(0, new SelectListItem { Value = "", Text = "Selecione o locatário" });
            ViewBag.Usuarios = usuarios;

            var removendo = _repositorio.GetEmprestadoById(id);

            if (removendo == null)
            {
                return RedirectToAction("ListaItens");
            }

            return View(removendo);
        }

        [HttpPost]
        public IActionResult RemocaoItemEmprestado(ItemEmprestado itemEmprestado)
        {
            itemEmprestado = _repositorio.GetEmprestadoById(itemEmprestado.Id);
            var itemVerificado = _repositorio.GetItemById(itemEmprestado.ItemId);
            itemVerificado.Disponivel = true;
            var item = itemVerificado;
            _repositorio.UpdateItem(item);
            _repositorio.Remove(itemEmprestado);

            return RedirectToAction("ListaItens");
        }
    }
}