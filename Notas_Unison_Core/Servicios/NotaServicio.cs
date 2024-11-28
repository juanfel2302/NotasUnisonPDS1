using System.IO;
using ClosedXML.Excel;
using Newtonsoft.Json;
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

    public void ExportarNotas(string filePath)
    {
        var notas = Listar(); // Obtener todas las notas de la base de datos
        string jsonContent = JsonConvert.SerializeObject(notas, Formatting.Indented);
        File.WriteAllText(filePath, jsonContent);

        // Generar un archivo Excel adicional
        string excelPath = Path.ChangeExtension(filePath, ".xlsx");
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.AddWorksheet("Notas");
            worksheet.Cell(1, 1).Value = "Nombre";
            worksheet.Cell(1, 2).Value = "Contenido";

            for (int i = 0; i < notas.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = notas[i].Nombre;
                worksheet.Cell(i + 2, 2).Value = notas[i].Contenido;
            }

            workbook.SaveAs(excelPath);
        }
    }
}