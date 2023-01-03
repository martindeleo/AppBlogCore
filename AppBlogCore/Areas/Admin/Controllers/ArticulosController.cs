using AppBlogCore.Data;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;   //Nota: para usar el SelectListItem necesito esta lib. Para versiones
using Microsoft.AspNetCore.Mvc;
using BlogCore.Models;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]   //Nota: ponemos esto
    public class ArticulosController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;
   

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = new BlogCore.Models.Articulo(),
                ListaCategorias =_contenedorTrabajo.Categoria.GetListaCategorias()
            };
            
            return View(artiVM);    //Nota se lo paso a la vista, asi mando todo, un articulo y su lista de cate
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)      //nota: recibimos este modelo !!
        {
            if(ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if(artiVM.Articulo.Id == 0)
                {
                    //nuevo articulo
                    string nombreArchivo = Guid.NewGuid().ToString(); //Nota: para colocar una cadena al inicio...por si existe algo con ese nombre..asi q le meto algo 
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");    //Nota: El articulo no lo guardo en la DB, asi q creeeo esa carpeta en mi proyecto
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension),FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                      

                    }
                    artiVM.Articulo.UrlImagen = @"\imagenes\\articulos\" + nombreArchivo + extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction("Index");
                }

            }
            //Si dio false :
            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(artiVM);

        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
           
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = new BlogCore.Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            if(id != null)
            {
                artiVM.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }
            

            return View(artiVM);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artiVM)      //nota: recibimos este modelo !!
        {
            string rutaImagen;

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articulosDesdeDb = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.Id);


                if (archivos.Count > 0) //Nota: Esto es porque por ahi en el edit no cambie el archivo
                {
                    //nuevo imagen
                    string nombreArchivo = Guid.NewGuid().ToString(); //Nota: para colocar una cadena al inicio...por si existe algo con ese nombre..asi q le meto algo 
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");    //Nota: El articulo no lo guardo en la DB, asi q creeeo esa carpeta en mi proyecto
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    rutaImagen = Path.Combine(rutaPrincipal, articulosDesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen)) 
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);


                    }
                    artiVM.Articulo.UrlImagen = @"\imagenes\\articulos\" + nombreArchivo + extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    //aqui es cuando la imagen es la misma
                    artiVM.Articulo.UrlImagen = articulosDesdeDb.UrlImagen;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                }
                _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction("Index");

            }
            //Si dio false :
            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(artiVM);

        }


        #region Llamadas a la API


        [HttpGet]
        public IActionResult GetAll()
        {
           
          return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }

        [HttpDelete]    //Nota: Nuevo verbo. No vamos a usar una vista para el borrado..sino un plugin
        public IActionResult Delete(int id)
        {
            string rutaPrincipal = _hostingEnviroment.WebRootPath;
            string rutaImagen;

            //Nota: El plugin esta seteado en Articulo.js y en layout
            var arti = _contenedorTrabajo.Articulo.Get(id);
            if (arti == null)
            {
                return Json(new { success = false, Message = "Error borrando articulo" });
            }

            rutaImagen = Path.Combine(rutaPrincipal, arti.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }


            _contenedorTrabajo.Articulo.Remove(arti);
            _contenedorTrabajo.Save();
            return Json(new { success = true, Message = "Articulo borrado con exito" });
        }

        #endregion
    }
}
