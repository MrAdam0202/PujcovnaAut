using System.Windows;       // Pro práci s oknem (zavření)
using System.Windows.Input; // Pro ICommand
using PujcovnaAut.Helpers;  // Pro RelayCommand
using DataEntity;    // Pro Entitu Kategorie

namespace PujcovnaAut.ViewModels
{
    public class KategorieEditViewModel : BaseVM
    {
        // Kategorie, kterou právě editujeme (nebo nová)
        public Kategorie EditovanaKategorie { get; set; }

        // Tlačítko "Uložit"
        public ICommand UlozitCommand { get; set; }

        // Konstruktor: Přijmeme kategorii, kterou chceme upravovat
        public KategorieEditViewModel(Kategorie kategorie)
        {
            EditovanaKategorie = kategorie;
            UlozitCommand = new RelayCommand(Ulozit);
        }

        private void Ulozit(object parameter)
        {
            // Zde by mohla být validace (např. jestli je vyplněný název)
            if (string.IsNullOrWhiteSpace(EditovanaKategorie.NazevKategorie))
            {
                MessageBox.Show("Musíte vyplnit název kategorie!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Pokud nám bylo předáno okno jako parametr, nastavíme mu DialogResult = true
            // Tím se okno zavře a řekne "bylo to úspěšné"
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}