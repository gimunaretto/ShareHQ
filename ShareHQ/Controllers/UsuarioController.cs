using Microsoft.AspNetCore.Mvc;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IRepositorio _repositorio;

        public UsuarioController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Usuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Usuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _repositorio.AdicionaUsuario(usuario);
                return RedirectToAction("Usuarios");
            }

            return View(usuario);
        }

        public IActionResult Usuarios()
        {
            var viewModel = new UsuariosViewModel()
            {
                Usuarios = _repositorio.Usuarios,
                Search = string.Empty
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Usuarios(UsuariosViewModel viewModel)
        {
            viewModel.Usuarios = _repositorio.Usuarios;

            return View(viewModel);
        }

        public IActionResult UsuarioEdicao(int id)
        {
            var usuarioEditando = _repositorio.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioEditando == null)
                return RedirectToAction("Usuarios");

            return View(usuarioEditando);
        }

        [HttpPost]
        public IActionResult UsuarioEdicao(Usuario usuario)
        {
            _repositorio.UpdateUsuario(usuario);

            return RedirectToAction("Usuarios");
        }

        public IActionResult UsuarioRemocao(int id)
        {
            var usuarioRemovendo = _repositorio.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioRemovendo == null)
                return RedirectToAction("Usuarios");

            return View(usuarioRemovendo);
        }

        [HttpPost]
        public IActionResult UsuarioRemocao(Usuario usuario)
        {
            _repositorio.RemoveUsuario(usuario);

            return RedirectToAction("Usuarios");
        }
    }
}