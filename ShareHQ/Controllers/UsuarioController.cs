using Microsoft.AspNetCore.Mvc;
using ShareHQ.Data;
using ShareHQ.Models;
using ShareHQ.ViewModels;

namespace ShareHQ.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IRepository<Usuario> _reposUsuario;

        public UsuarioController(IRepository<Usuario> reposUsuario)
        {
            _reposUsuario = reposUsuario;
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
                _reposUsuario.Add(usuario);
                return RedirectToAction("Usuarios");
            }

            return View(usuario);
        }

        public IActionResult Usuarios()
        {
            var viewModel = new UsuariosViewModel()
            {
                Usuarios = _reposUsuario.GetAll(),
                Search = string.Empty
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Usuarios(UsuariosViewModel viewModel)
        {
            viewModel.Usuarios = _reposUsuario.GetAll();

            return View(viewModel);
        }

        public IActionResult UsuarioEdicao(int id)
        {
            var usuarioEditando = _reposUsuario.GetById(id);

            if (usuarioEditando == null)
                return RedirectToAction("Usuarios");

            return View(usuarioEditando);
        }

        [HttpPost]
        public IActionResult UsuarioEdicao(Usuario usuario)
        {
            _reposUsuario.Update(usuario);

            return RedirectToAction("Usuarios");
        }

        public IActionResult UsuarioRemocao(int id)
        {
            var usuarioRemovendo = _reposUsuario.GetById(id);

            if (usuarioRemovendo == null)
                return RedirectToAction("Usuarios");

            return View(usuarioRemovendo);
        }

        [HttpPost]
        public IActionResult UsuarioRemocao(Usuario usuario)
        {
            _reposUsuario.Remove(usuario);

            return RedirectToAction("Usuarios");
        }
    }
}