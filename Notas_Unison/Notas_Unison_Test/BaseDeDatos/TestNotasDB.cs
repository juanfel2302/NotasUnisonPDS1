using Notas_Unison_Core.BaseDeDatos;

namespace Notas_Unison_Test.BaseDeDatos;

public class TestNotasDB
{
    [Test]
    public void PruebaAgregarProducto()
    {
        // 1. Crear la conexión con la base de datos.
        await using var db = new NotasDB(); 
        
        // 2. Eliminar el contenido de la base de datos.
        db.Database.EnsureDeleted();
        
        // 3. Asegurar que la base de datos exista.
        db.Database.EnsureCreated();
       
        // 4. Crear id del producto.
        var id = Guid.NewGuid();
        
        // 5. Crear un producto.
        Producto producto = new()
        {
            Id = id,
            Nombre = "Producto 1",
            Descripcion = "Descripcion 1",
            Precio = 500f
        };

        // 6. Añadir el producto a la base de datos.
        db.Productos.Add(producto);
        db.SaveChanges();
        
        // 7. Consultar los productos para comprobar que se añadió el producto.
        var resultado = db.Productos.Find(id);
        
        Assert.That(resultado, Is.Not.Null, "No se agregó el producto.");
        Assert.That(resultado.Id, Is.EqualTo(id), "Producto con Id incorrecto.");
    }

}

