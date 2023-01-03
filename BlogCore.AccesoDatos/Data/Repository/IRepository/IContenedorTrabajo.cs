using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo :IDisposable
    {
        ICategoriaRepository Categoria { get; }     //Nota: lo mismo para  articulo etc

        //Nota: aca vienen el resto de los reposutorios

        IArticuloRepository Articulo { get; }

        ISliderRepository Slider { get; }


        void Save();
    }
}
