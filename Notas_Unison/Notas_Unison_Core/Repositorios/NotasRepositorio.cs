using Notas_Unison_Core.BaseDeDatos;
using Notas_Unison_Core.Modelos;
using Unison_Almacen_Core.Contratos.Repositorios;

namespace Notas_Unison_Core.Repositorios;

public class NotasRepositorio : IRepositorio<Notas>
{
    public void Agregar(Notas notaNueva)
    {
        using var bd = new NotasDB();
        
        bd.Notas.Add(notaNueva);
        
        bd.SaveChanges();
    }

    public List<Notas> Listar()
    {
        using var bd = new NotasDB();
        
        return bd.Notas.ToList();
    }

    public Notas ObtenerPorId(Guid id)
    {
        using var bd = new NotasDB();
        
        return bd.Notas.Find(id);
    }

    public void Modificar(Notas notaModificacion)
    {
        using var bd = new NotasDB();
        
        bd.Notas.Update(notaModificacion);

        bd.SaveChanges();
    }

    public void Eliminar(Notas notaAeliminar)
    {
        using var bd = new NotasDB();

        bd.Notas.Remove(notaAeliminar);

        bd.SaveChanges();
    }
}