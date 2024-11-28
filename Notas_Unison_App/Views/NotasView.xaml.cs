using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Notas_Unison_Core.Modelos;
using Notas_Unison_Core.Servicios;
using Notas_Unison.ViewModel;

namespace Notas_Unison.Views;

public partial class NotasView : Page
{
    public NotasView(NotasViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;

    }


    private void NotaNueva(object sender, RoutedEventArgs e)
    {
        // Obtener la instancia del servicio de AddNotasViewModel desde el contenedor de dependencias
        var addNotasViewModel = App.Current.Services.GetRequiredService<AddNotasViewModel>();
    
        // Crear una instancia de AddNotaView pasando el viewModel
        var addNotaView = new AddNotaView(addNotasViewModel);
    
        // Navegar a la vista AddNotaView
        NavigationService.Navigate(addNotaView);
    }

    private void EditarNota(object sender, RoutedEventArgs e)
    {
        // Obtener la nota seleccionada
        var button = sender as Button;
        var notaSeleccionada = button?.DataContext as Notas;

        if (notaSeleccionada != null)
        {
            // Obtener el servicio de EditarNotasViewModel desde el contenedor de dependencias
            var editarNotasViewModel = App.Current.Services.GetRequiredService<EditarNotasViewModel>();

            // Asignar la nota seleccionada al ViewModel de la vista de edición
            editarNotasViewModel.NotaSeleccionada = notaSeleccionada;

            // Crear la vista de edición pasando el ViewModel
            var editarNotasView = new EditarNotasView(editarNotasViewModel);

            // Navegar a la vista de edición
            NavigationService.Navigate(editarNotasView);
        }
    }
}