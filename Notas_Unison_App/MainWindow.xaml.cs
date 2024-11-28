using Notas_Unison.ViewModel;
using Notas_Unison.Views;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Notas_Unison;

public partial class MainWindow : FluentWindow
{
    public MainWindow(MainWindowViewModel viewModel, IPageService pageService)
    {
        // Inicializamos el contexto de datos.
        DataContext = viewModel;
        
        // Inicializamos los componentes de la ventana.
        InitializeComponent();

        // Inicializa el tema de la aplicación.
        ApplicationThemeManager.Apply(this);

        // Establece la página de inicio.
        RootNavigation.SetPageService(pageService);
        
        // Iniciamos la aplicación en la vista de inicio.
        Loaded += (_, _) => RootNavigation.Navigate(typeof(InicioView));

    }
}