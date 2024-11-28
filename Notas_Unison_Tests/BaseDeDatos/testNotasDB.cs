using Notas_Unison_Core.BaseDeDatos;
using Notas_Unison_Core.Modelos;

namespace Notas_Unison_Test.BaseDeDatos;

public class PruebasNotasDB
{
    [Test]
    public void PruebaAgregarNota()
    {
        // 1. Crear la conexión con la base de datos.
        using var db = new NotasDB(); 
        
        // 2. Eliminar el contenido de la base de datos.
        db.Database.EnsureDeleted();
        
        // 3. Asegurar que la base de datos exista.
        db.Database.EnsureCreated();
       
       // 4. Crear id de la nota.
       var id = Guid.NewGuid();
        
       // 5. Crear una nota.
       Notas nota = new()
       {
           Id = id,
           Nombre = "Nota 1",
           Contenido = "Contenido 1",
       };

       // 6. Añadir la nota a la base de datos.
       db.Notas.Add(nota);
       db.SaveChanges();
        
       // 7. Consultar las notas para comprobar que se añadió la nota.
       var resultado = db.Notas.Find(id);
        
       Assert.That(resultado, Is.Not.Null, "No se agregó la nota.");
       Assert.That(resultado.Id, Is.EqualTo(id), "Nota con Id incorrecto.");
    }
    
    [Test]
    public void PruebaModificarNota()
    {
        // 1. Crear la conexión con la base de datos.
        using var db = new NotasDB(); 
        
        // 2. Eliminar el contenido de la base de datos.
        db.Database.EnsureDeleted();
        
        // 3. Asegurar que la base de datos exista.
        db.Database.EnsureCreated();
       
        // 4. Crear id de la nota.
        var id = Guid.NewGuid();
        
        // 5. Crear una nota.
        Notas nota = new()
        {
            Id = id,
            Nombre = "Nota 1",
            Contenido = "Los Datos del Nota 1 son los siguientes " +
                        "1.- Queso para quesadillas",
        };

        // 6. Añadir la nota a la base de datos.
        db.Notas.Add(nota);
        db.SaveChanges();
        
        // 7. Modificar la nota.
        var nuevoNombre = "Nota 2";
        nota.Nombre = nuevoNombre;
        
        // 8. Guardar cambios en la bd.
        db.Notas.Update(nota);
        db.SaveChanges();
        
        // 9. Consultar las notas para comprobar que se modificó la nota.
        var resultado = db.Notas.Find(id);
        
        Assert.That(resultado, Is.Not.Null, "No se modificó la nota.");
        Assert.That(resultado.Id, Is.EqualTo(id), "Nota con Id incorrecto.");
        Assert.That(resultado.Nombre, Is.EqualTo(nuevoNombre), "Nota con Nombre incorrecto.");
    }
    
    [Test]
    public void PruebaEliminarNota()
    {
        // 1. Crear la conexión con la base de datos.
        using var db = new NotasDB(); 
        
        // 2. Eliminar el contenido de la base de datos.
        db.Database.EnsureDeleted();
        
        // 3. Asegurar que la base de datos exista.
        db.Database.EnsureCreated();
       
        // 4. Crear id de la nota.
        var id = Guid.NewGuid();
        
        // 5. Crear una nota.
        Notas nota = new()
        {
            Id = id,
            Nombre = "Nota 1",
            Contenido = "Contenido 1",
        };

        // 6. Añadir la nota a la base de datos.
        db.Notas.Add(nota);
        db.SaveChanges();
        
        // 7. Consultar las notas para comprobar que se añadió la nota.
        var resultado = db.Notas.Find(id);
        
        // 8. Comprobar que la nota se añadió correctamente.
        Assert.That(resultado, Is.Not.Null, "No se agregó la nota.");
        Assert.That(resultado.Id, Is.EqualTo(id), "Nota con Id incorrecto.");
        
        // 9. Eliminar la nota de la base de datos.
        db.Notas.Remove(nota);
        db.SaveChanges();
        
        // 10. Comprobar que la nota se eliminó de la base de datos.
        Assert.That(db.Notas.Find(id), Is.Null, "Nota aún existe.");
    }
}
