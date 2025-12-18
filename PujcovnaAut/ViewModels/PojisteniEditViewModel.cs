using System.Windows;
using System.Windows.Input;
using PujcovnaAut.Helpers;
using PujcovnaAut.Model;

namespace PujcovnaAut.ViewModels
{
    public class PojisteniEditViewModel : BaseVM
    {
        // Pojištění, které upravujeme
        public Pojisteni EditovanePojisteni { get; set; }

        public ICommand UlozitCommand { get; set; }

        public PojisteniEditViewModel(Pojisteni pojisteni)
        {
            EditovanePojisteni = pojisteni;
            UlozitCommand = new RelayCommand(Ulozit);
        }

        private void Ulozit(object parameter)
        {
            // Jednoduchá validace
            if (string.IsNullOrWhiteSpace(EditovanePojisteni.NazevPlanu))
            {
                MessageBox.Show("Musíte vyplnit název plánu!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Zavření okna s výsledkem "true"
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}