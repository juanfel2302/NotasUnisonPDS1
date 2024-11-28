using System.Windows.Controls;
using Notas_Unison_Core.Modelos;
using Notas_Unison.ViewModel;

namespace Notas_Unison.Views;

public partial class EditarNotasView : Page
{
    public EditarNotasView(EditarNotasViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}