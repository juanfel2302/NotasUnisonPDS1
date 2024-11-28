using Notas_Unison_Core.Modelos;

namespace Notas_Unison_Test.Modelos;

public class TestNotas
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaNotaId()
    {
        // 1. Crear una nota.
        var nota = new Notas();

        // 2. Asignarle un Id.
        var notaId = Guid.NewGuid();
        nota.Id = notaId;

        // 3. Comprobar que el Id asignado es el mismo
        //    que devuelve.
        Assert.That(nota.Id, Is.EqualTo(notaId));
    }

    [Test]
    public void PruebaNotaNombre()
    {
        /*
         * 1. Crear una nota.
         * 2. Asignarle un nombre.
         * 3. Comprobar que el nombre asignado es el
         *    mismo que devuelve.
         */

        // 1. Crear una nota.
        var nota = new Notas();

        // 2. Asignarle un nombre.
        const string nombreNota = "Nota importante sobre ventas";
        nota.Nombre = nombreNota;

        // 3. Comprobar que el nombre asignado es el
        //    mismo que devuelve.
        Assert.That(nota.Nombre, Is.EqualTo(nombreNota));
    }

    [Test]
    public void PruebaNotaContenido()
    {
        // 1. Crear una nota.
        var nota = new Notas();

        // 2. Asignar el contenido.
        const string contenidoNota = "Este es el contenido de la nota.";
        nota.Contenido = contenidoNota;

        // 3. Comprobar que el contenido asignado es el
        //    mismo que devuelve.
        Assert.That(nota.Contenido, Is.EqualTo(contenidoNota));
    }
}

   