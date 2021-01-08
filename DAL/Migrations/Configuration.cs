namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.DB_ExamenContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.DB_ExamenContext context)
        {
            context.Articulos.AddOrUpdate(x => x.Id,
                new Articulo() { Descripcion = "Camiseta Unisex XL", Precio = 700, Stock = 150, Activo = true },
                new Articulo() { Descripcion = "Camiseta Unisex L", Precio = 650, Stock = 100, Activo = true },
                new Articulo() { Descripcion = "Camiseta Unisex S", Precio = 500, Stock = 90, Activo = true },
                new Articulo() { Descripcion = "Bermuda Hombre", Precio = 1500, Stock = 30, Activo = true },
                new Articulo() { Descripcion = "Campera Polar Unisex", Precio = 5000, Stock = 50, Activo = false },
                new Articulo() { Descripcion = "Bufanda Unisex", Precio = 1200, Stock = 20, Activo = false },
                new Articulo() { Descripcion = "Short Mujer", Precio = 1400, Stock = 0, Activo = true },
                new Articulo() { Descripcion = "Medias Polares Unisex", Precio = 900, Stock = 0, Activo = false },
                new Articulo() { Descripcion = "Cinto cuero", Precio = 800, Stock = 5, Activo = true }
             ) ;
        }
    }
}
