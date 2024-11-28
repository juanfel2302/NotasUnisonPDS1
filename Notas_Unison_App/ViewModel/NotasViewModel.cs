using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Notas_Unison_Core.Contratos.Servicios;
using Notas_Unison_Core.Modelos;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Notas_Unison.ViewModel
{
    public partial class NotasViewModel : ObservableObject
    {
        private ObservableCollection<Notas> _notas;

        public ObservableCollection<Notas> Notas
        {
            get => _notas;
            set => SetProperty(ref _notas, value);
        }


        // Comando para eliminar la nota
        public IRelayCommand<Notas> EliminarCommand { get; }
        public IRelayCommand<Notas> ExportarNotasCommand { get; }
        
        public IRelayCommand ImportarNotasCommand { get; }



      
        private readonly IServicio<Notas> _servicio;

        // Constructor del ViewModel
        public NotasViewModel(IServicio<Notas> servicio)
        {
            _servicio = servicio;

            // Inicializar el comando de eliminación en el constructor
            EliminarCommand = new RelayCommand<Notas>(Eliminar);
            ExportarNotasCommand = new RelayCommand<Notas>(ExportarNotas);
            ImportarNotasCommand = new RelayCommand(ImportarNotas);

    

            // Obtener las notas desde el servicio (esto se asume que devuelve una lista de notas)
            Notas = new ObservableCollection<Notas>(_servicio.Listar());
        }

        // Método que maneja la eliminación de la nota

        private void Eliminar(Notas nota)
        {
            if (nota == null)
            {
                MessageBox.Show("No se seleccionó ninguna nota para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Mostrar cuadro de confirmación
            var result = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar la nota '{nota.Nombre}'?",
                "Confirmación de eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            // Si el usuario confirma la eliminación
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Eliminar la nota de la base de datos
                    _servicio.Eliminar(nota);

                    // Refrescar la lista observable
                    Notas = new ObservableCollection<Notas>(_servicio.Listar());

                    MessageBox.Show("Nota eliminada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la nota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ExportarNotas(Notas nota)
        {
            if (nota == null)
            {
                MessageBox.Show("No se seleccionó ninguna nota para exportar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear un cuadro de diálogo para guardar el archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos de Notas (*.notasunison)|*.notasunison|Todos los archivos (*.*)|*.*",
                Title = "Exportar Nota",
                FileName = $"{nota.Nombre}.notasunison"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Convertir la nota a JSON y guardarla en el archivo
                    string contenido = Newtonsoft.Json.JsonConvert.SerializeObject(nota, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filePath, contenido);

                    MessageBox.Show("Nota exportada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar la nota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ImportarNotas()
        {
            // Crear un cuadro de diálogo para abrir el archivo
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de Notas (*.notasunison)|*.notasunison|Todos los archivos (*.*)|*.*",
                Title = "Importar Nota"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Leer el archivo y deserializar una sola nota
                    string contenido = File.ReadAllText(filePath);
                    var notaImportada = Newtonsoft.Json.JsonConvert.DeserializeObject<Notas>(contenido);

                    if (notaImportada != null)
                    {
                        // Agregar la nota importada a la base de datos
                        _servicio.Agregar(notaImportada); // Asumiendo que tienes un método Agregar en tu servicio para agregar la nota

                        // Refrescar la lista observable
                        Notas = new ObservableCollection<Notas>(_servicio.Listar());

                        MessageBox.Show("Nota importada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El archivo no contiene una nota válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al importar la nota: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}