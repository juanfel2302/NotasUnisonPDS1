using Notas_Unison_Core.BaseDeDatos;
using Notas_Unison_Core.Modelos;
using Notas_Unison_Core.Repositorios;
using Notas_Unison_Core.Servicios;

namespace Notas_Unison_Tests.Repositorio;

public class RepositorioTest
{
    [Test]
    public void TestarRepositorio()
    {
        // Asegurarse que la base de datos se reinicie para cada prueba
        using var db = new NotasDB();
        db.Database.EnsureDeleted();  // Limpiar
        db.Database.EnsureCreated();  // Crear base de datos nueva

        var repo = new NotasRepositorio();
        var servicio = new NotaServicio(repo);

        var idNota = Guid.NewGuid();
        var nota = new Notas { Id = idNota, Nombre = "Test", Contenido = "Contenido" };

        // Agregar la nota
        servicio.Agregar(nota);

        // Listar las notas y verificar
        var notas = servicio.Listar();
        Assert.That(notas.Count, Is.EqualTo(1), "La lista de notas debería contener una nota.");
        Assert.That(notas[0].Id, Is.EqualTo(idNota), "El ID de la nota agregada no coincide.");
    }
    [Test]
    public void EliminarNotaTest()
    {
        // Asegurarse que la base de datos se reinicie para cada prueba
        using var db = new NotasDB();
        db.Database.EnsureDeleted();  // Limpiar
        db.Database.EnsureCreated();  // Crear base de datos nueva

        var repo = new NotasRepositorio();
        var servicio = new NotaServicio(repo);

        var idNota = Guid.NewGuid();
        var nota = new Notas { Id = idNota, Nombre = "Test", Contenido = "Contenido" };

        // Agregar la nota
        servicio.Agregar(nota);

        // Verificar que la nota fue agregada
        var notas = servicio.Listar();
        Assert.That(notas.Count, Is.EqualTo(1), "La lista de notas debería contener una nota.");

        // Eliminar la nota
        servicio.Eliminar(nota);

        // Verificar que la lista de notas esté vacía después de la eliminación
        var notasDespuesDeEliminar = servicio.Listar();
        Assert.That(notasDespuesDeEliminar.Count, Is.EqualTo(0), "La lista de notas debería estar vacía después de eliminar la nota.");
    }
}

    
