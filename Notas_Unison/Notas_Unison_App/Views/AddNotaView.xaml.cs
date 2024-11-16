using System.Windows;
using System.Windows.Controls;
using Notas_Unison.ViewModel;

namespace Notas_Unison.Views;

public partial class AddNotaView : Page
{
    public AddNotaView(AddNotasViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}