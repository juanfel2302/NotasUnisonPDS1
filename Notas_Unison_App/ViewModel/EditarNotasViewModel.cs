using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Notas_Unison_Core.Contratos.Servicios;
using Notas_Unison_Core.Modelos;
using System.Collections.Generic;

namespace Notas_Unison.ViewModel
{
    public partial class EditarNotasViewModel : ObservableObject
    {
        private readonly IServicio<Notas> _servicio;

        // Lista de todas las notas
        public List<Notas> _notas;
        public List<Notas> Notas
        {
            get => _notas;
            set => SetProperty(ref _notas, value);
        }

        // Nota seleccionada
        private Notas _notaSeleccionada;
        public Notas NotaSeleccionada
        {
            get => _notaSeleccionada;
            set => SetProperty(ref _notaSeleccionada, value);
        }

        // Comando para guardar la nota
        public IRelayCommand GuardarNotaCommand { get; }

        // Constructor
        public EditarNotasViewModel(IServicio<Notas> servicio)
        {
            _servicio = servicio;

            // Cargar las notas al inicializar el ViewModel
            Notas = _servicio.Listar();

            // Inicializar el comando para guardar
            GuardarNotaCommand = new RelayCommand(GuardarNota);
        }

        // Método para guardar la nota seleccionada
        private void GuardarNota()
        {
            if (NotaSeleccionada != null)
            {
                _servicio.Modificar(NotaSeleccionada); 
            }
        }
    }
}