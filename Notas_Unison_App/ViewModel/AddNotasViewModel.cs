using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Notas_Unison_Core.Contratos.Servicios;
using Notas_Unison_Core.Modelos;

namespace Notas_Unison.ViewModel;

public partial class AddNotasViewModel : ObservableObject
{
    [ObservableProperty] private Notas _nota = new Notas();
    [ObservableProperty] private List<Notas> _notas;
    


    [ObservableProperty] private string _txtBotonFormulario;

    // Propiedad para el color seleccionado
    [ObservableProperty] private SolidColorBrush _selectedColor;

    // Lista de colores disponibles
    public ObservableCollection<SolidColorBrush> AvailableColors { get; } = new ObservableCollection<SolidColorBrush>
    {
        new SolidColorBrush(Colors.Pink),
        new SolidColorBrush(Colors.Green),
        new SolidColorBrush(Colors.Yellow),
        new SolidColorBrush(Colors.Gray),
        new SolidColorBrush(Colors.LightBlue)
    };

    private readonly IServicio<Notas> _servicio;

    public AddNotasViewModel(IServicio<Notas> servicio)
    {
        // Inicializar comandos
        AgregarNotaCommand = new RelayCommand(AgregarNota);

        // Guardar la referencia del servicio
        _servicio = servicio;

        // Obtener las notas de la base de datos y manejar posibles valores nulos
        _notas = _servicio.Listar() ?? new List<Notas>();

        // Inicializar propiedades
        SelectedColor = new SolidColorBrush(Colors.White); 
    }
    
    public ICommand AgregarNotaCommand { get; }

    private void AgregarNota()
    {
        var n = Nota;

        if (n == null)
        {
            Console.WriteLine("Nota es null.");
            return;
        }
        

        Console.WriteLine($"Nombre: '{n.Nombre}', Contenido: '{n.Contenido}'");

        // Verificación de campos vacíos
        if (string.IsNullOrWhiteSpace(n.Nombre) || string.IsNullOrWhiteSpace(n.Contenido))
        {
            Console.WriteLine("No se puede agregar la nota: el nombre o el contenido están vacíos.");
            return;
        }

        try
        {
            // Si la nota tiene un Id vacío, asignar un nuevo Id
            if (n.Id == Guid.Empty)
            {
                n.Id = Guid.NewGuid();
            }
            

            var notas = _servicio.ObtenerPorId(n.Id);

            // Si la nota no existe (n.Id es vacío o n.Id no se encuentra), entonces es una nueva nota
            if (notas == null || notas.Id == Guid.Empty)
            {
                // Agregar la nota al repositorio
                _servicio.Agregar(n);

                // Mostrar mensaje de éxito en la consola
                var result = MessageBox.Show("Nota agregada Correctamente");
               
            }else
            {
                Console.WriteLine("La nota con el mismo ID ya existe.");
            }
        }
        catch (Exception ex)
        {
            // Capturar y mostrar el error en la consola
            Console.WriteLine($"Error al agregar la nota: {ex.Message}");
        }
    }
}
