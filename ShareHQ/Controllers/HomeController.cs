using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ShareHQ.Models;
using ShareHQ.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace ShareHQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorio _repositorio;

        public HomeController(ILogger<HomeController> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
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
                _repositorio.AdicionaUsuario(usuario);
                return RedirectToAction("Index");
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
                _repositorio.AdicionaCategoria(categoria);
                return RedirectToAction("Index");
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
        #endregion

        #region [Itens]
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
            _repositorio.Add(item);
            return RedirectToAction("Index");
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
            _repositorio.Update(item);
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
            _repositorio.Remove(item);

            return RedirectToAction("Itens");
        }

        private void MontaDropDownListCatergoria()
        {
            var categorias = _repositorio.GetCategorias().Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
            categorias.Insert(0, new SelectListItem { Value = "", Text = "Selecione Categoria" });
            ViewBag.Categorias = categorias;
        }
        #endregion

        #region [Emprestimo de Item]
        public IActionResult Emprestimo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Emprestimo(ItemEmprestado itemEmprestado)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(itemEmprestado);
        }
        #endregion
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
