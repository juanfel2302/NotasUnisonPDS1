using CommunityToolkit.Mvvm.ComponentModel;
using Notas_Unison_Core.Contratos.Servicios;
using Notas_Unison_Core.Modelos;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace Notas_Unison.ViewModel
{
    public class NotasViewModel : ObservableObject
    {
        private List<Notas> _notas;

        public List<Notas> Notas
        {
            get => _notas;
            set => SetProperty(ref _notas, value);
        }

        private readonly IServicio<Notas> _servicio;

        public NotasViewModel(IServicio<Notas> servicio)
        {
            _servicio = servicio;

            // Mostrar Las Notas Previamente Agregadas
            Notas = _servicio.Listar();
        }
        
    }
}
