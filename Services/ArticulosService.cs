using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Services
{
    public class ArticulosService : IArticulosService
    {
        private readonly DB_ExamenContext context;
        public ArticulosService(/*TODO Usar iyeccion de dependencia*/)
        {
            this.context = new DB_ExamenContext();
        }
        public Articulo Add(Articulo newArticulo)
        {
            Articulo addedArticulo = this.context.Articulos.Add(newArticulo);
            this.context.SaveChanges();
            if (addedArticulo.Id != 0)
                return addedArticulo;
            return null;
        }

        public bool Delete(int id)
        {
            Articulo deletedArticulo = GetById(id);
            if (deletedArticulo != null)
            {
                this.context.Articulos.Remove(deletedArticulo);
                var result = this.context.SaveChanges();
                return result != 0;
            }
            return false;
        }

        public IEnumerable<Articulo> GetAll()
        {
            return context.Articulos.ToList();
        }

        public Articulo GetById(int id)
        {
            return context.Articulos.Find(id);
        }

        public Articulo Update(int id, Articulo updatedArticulo)
        {
            Articulo articulo = GetById(id);
            if(articulo != null)
            {
                articulo.Descripcion = updatedArticulo.Descripcion;
                articulo.Precio = updatedArticulo.Precio;
                articulo.Stock = updatedArticulo.Stock;
                articulo.Activo = updatedArticulo.Activo;

                this.context.SaveChanges();
                return articulo;
                
            }
            return null;
        }
    }
}
