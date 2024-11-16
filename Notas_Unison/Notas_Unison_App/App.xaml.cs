using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Notas_Unison_Core.BaseDeDatos;
using Notas_Unison_Core.Contratos.Servicios;
using Notas_Unison_Core.Modelos;
using Notas_Unison_Core.Repositorios;
using Notas_Unison_Core.Servicios;
using Notas_Unison.Servicios;
using Notas_Unison.ViewModel;
using Notas_Unison.Views;
using Unison_Almacen_Core.Contratos.Repositorios;
using Wpf.Ui;

namespace Notas_Unison;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        // Inicializar los servicios.
        Services = ConfigServices();
        
        // Asegurar que la base de datos existe.
        using var bd = new NotasDB();
        bd.Database.EnsureCreated();
    }
    
    /// <summary>
    /// Obtiene la referencia de la aplicación actual.
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Obtiene el proveedor para resolver los servicios de la aplicación.
    /// </summary>
    public IServiceProvider Services { get; }
    
    /// <summary>
    /// Configura los servicios de la aplicación.
    /// </summary>
    /// <returns></returns>
    private static IServiceProvider ConfigServices()
    {
        var services = new ServiceCollection();
        
        // Servicios.
        services.AddTransient<NotasDB>();
        services.AddTransient<IServicio<Notas>, NotaServicio>();
        services.AddTransient<IRepositorio<Notas>, NotasRepositorio>();
        services.AddSingleton<IPageService, PageService>();

        // MainWindow.
        services.AddSingleton<MainWindow>();
                
        // Views
        services.AddTransient<AddNotaView>();
        services.AddTransient<InicioView>();
        services.AddTransient<NotasView>();
        services.AddTransient<EditarNotasView>();
        
        // ViewModels.
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<AddNotasViewModel>();
        services.AddTransient<NotasViewModel>();
        services.AddTransient<EditarNotasViewModel>();

        return services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}