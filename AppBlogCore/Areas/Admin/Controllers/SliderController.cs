using AppBlogCore.Data;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;   //Nota: para usar el SelectListItem necesito esta lib. Para versiones
using Microsoft.AspNetCore.Mvc;
using BlogCore.Models;
using System.Security.Policy;

namespace AppBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]   //Nota: ponemos esto
    public class SliderController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public SliderController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;
        }

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
        public IActionResult Create(Slider slider)      //nota: recibimos este modelo !!
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (slider.Id == 0)
                {
                    //nuevo slider
                    string nombreArchivo = Guid.NewGuid().ToString(); //Nota: para colocar una cadena al inicio...por si existe algo con ese nombre..asi q le meto algo 
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");    //Nota: El articulo no lo guardo en la DB, asi q creeeo esa carpeta en mi proyecto
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);


                    }
                    slider.UrlImagen = @"\imagenes\\sliders\" + nombreArchivo + extension;
                   

                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction("Index");
                }

            }
            //Si dio false :
            return View();

        }


        public IActionResult Edit(int id)
        {
            var sli = _contenedorTrabajo.Slider.Get(id);

            return View(sli);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider sli) //nota: recibimos este modelo !!
        {
            string rutaImagen;

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var SliDesdeDb = _contenedorTrabajo.Slider.Get(sli.Id);


                if (archivos.Count > 0) //Nota: Esto es porque por ahi en el edit no cambie el archivo
                {
                    //nuevo imagen
                    string nombreArchivo = Guid.NewGuid().ToString(); //Nota: para colocar una cadena al inicio...por si existe algo con ese nombre..asi q le meto algo 
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");    //Nota: El slider no lo guardo en la DB, asi q creeeo esa carpeta en mi proyecto
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    rutaImagen = Path.Combine(rutaPrincipal, SliDesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);


                    }
                    sli.UrlImagen = @"\imagenes\\sliders\" + nombreArchivo + extension;
                    

                    _contenedorTrabajo.Slider.Update(sli);
                    _contenedorTrabajo.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    //aqui es cuando la imagen es la misma
                    sli.UrlImagen = SliDesdeDb.UrlImagen;
                   
                }
                _contenedorTrabajo.Slider.Update(sli);
                _contenedorTrabajo.Save();

                return RedirectToAction("Index");

            }
            //Si dio false :
            return View();

        }



        #region Llamadas a la API


        [HttpGet]
        public IActionResult GetAll()
        {

            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }

        [HttpDelete]    //Nota: Nuevo verbo. No vamos a usar una vista para el borrado..sino un plugin
        public IActionResult Delete(int id)
        {
            string rutaPrincipal = _hostingEnviroment.WebRootPath;
            string rutaImagen;

            //Nota: El plugin esta seteado en Articulo.js y en layout
            var arti = _contenedorTrabajo.Slider.Get(id);
            if (arti == null)
            {
                return Json(new { success = false, Message = "Error borrando slider" });
            }

            rutaImagen = Path.Combine(rutaPrincipal, arti.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }


            _contenedorTrabajo.Slider.Remove(arti);
            _contenedorTrabajo.Save();
            return Json(new { success = true, Message = "slider borrado con exito" });
        }

        #endregion

    }
}
