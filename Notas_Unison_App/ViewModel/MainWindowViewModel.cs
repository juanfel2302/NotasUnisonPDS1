using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Notas_Unison.Views;
using Wpf.Ui.Controls;

namespace Notas_Unison.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<NavigationViewItem> _navigationItems = [];

    public MainWindowViewModel()
    {
        NavigationItems =
        [
            new NavigationViewItem()
            {
                Content = "Notas",
                TargetPageType = typeof(NotasView)
            },
            new NavigationViewItem()
            {
                Content = "Añadir Nota",
                TargetPageType = typeof(AddNotaView)
            },
            new NavigationViewItem()
            {
            Content = "EditarNota",
            TargetPageType = typeof(EditarNotasView)
            }
        ];
    }
}