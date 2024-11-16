using System.Windows;
using System.Windows.Controls;
using Notas_Unison.ViewModel;

namespace Notas_Unison.Views;

public partial class NotasView : Page
{
    public NotasView(NotasViewModel viewModel)
    {
        InitializeComponent();
        
        DataContext = viewModel;

    }

    
}