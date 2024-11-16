using Notas_Unison_Core.Contratos.Servicios;
using Notas_Unison_Core.Modelos;
using Unison_Almacen_Core.Contratos.Repositorios;

namespace Notas_Unison_Core.Servicios;

public class NotaServicio(IRepositorio<Notas> repositorio) : IServicio<Notas>
{
    public void Agregar(Notas notaNueva)
    {
        repositorio.Agregar(notaNueva);
    }

    public List<Notas> Listar()
    {
        return repositorio.Listar();
    }

    public Notas ObtenerPorId(Guid id)
    {
        return repositorio.ObtenerPorId(id);
    }

    public void Modificar(Notas notaModificacion)
    {
        repositorio.Modificar(notaModificacion);
    }

    public void Eliminar(Notas notaAEliminar)
    {
        repositorio.Eliminar(notaAEliminar);
    }
}