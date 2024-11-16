namespace Notas_Unison_Core.Contratos.Servicios;

public interface IServicio<T>
{
    void Agregar(T notaNueva);
    List<T> Listar();
    T ObtenerPorId(Guid id);
    void Modificar(T notaModificacion);
    void Eliminar(T notaAEliminar);
}