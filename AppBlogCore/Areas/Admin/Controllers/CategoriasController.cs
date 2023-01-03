using AppBlogCore.Data;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]   //Nota: ponemos esto
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
       // private readonly ApplicationDbContext _context;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo    )
        {
            _contenedorTrabajo = contenedorTrabajo;
           // _context = context;
        }

        [HttpGet]   //Nota: Es decoracion
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]   
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {


            if(categoria == null) { 
                return View();

            }

            if(!ModelState.IsValid)
            {
                return View();
            }
            _contenedorTrabajo.Categoria.Add(categoria);
            _contenedorTrabajo.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria;
           // categoria = _contenedorTrabajo.Categoria.GetFirstOrDefault(x => x.Id == id); Opcion 1
            categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria != null)
            {
                return View(categoria);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {


            if (categoria == null)
            {
                return View();

            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            _contenedorTrabajo.Categoria.Update(categoria);
            _contenedorTrabajo.Save();

            return RedirectToAction("Index");
        }





        #region Llamadas a la API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Opcion 1
            return Json(new {data = _contenedorTrabajo.Categoria.GetAll()});
        }

        [HttpDelete]    //Nota: Nuevo verbo. No vamos a usar una vista para el borrado..sino un plugin
        public IActionResult Delete(int id)
        {
            //Nota: El plugin esta seteado en Categoria.js y en layout
            var cate = _contenedorTrabajo.Categoria.Get(id);
            if (cate == null)
            {
                return Json(new { success = false, Message = "Error borrando categoria" });
            }
            _contenedorTrabajo.Categoria.Remove(cate);
            _contenedorTrabajo.Save();
            return Json(new { success = true, Message = "Categoria borrad con exito" });
        }

        #endregion
    }
}
