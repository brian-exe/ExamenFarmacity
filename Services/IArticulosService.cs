using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    public interface IArticulosService
    {
        IEnumerable<Articulo> GetAll();
        Articulo GetById(int id);
        Articulo Add(Articulo newArticulo);
        Articulo Update(int id, Articulo updatedArticulo);
        bool Delete(int id);
    }
}
