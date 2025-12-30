using System.Windows;
using System.Windows.Input;
using PujcovnaAut.Helpers;
using DataEntity;

namespace PujcovnaAut.ViewModels
{
    /// <summary>
    /// ViewModel pro okno editace kategorií vozidel.
    /// </summary>
    public class KategorieEditViewModel : BaseVM
    {
        // Je definována instance kategorie, která je předmětem editace nebo vytvoření.
        public Kategorie EditovanaKategorie { get; set; }

        // Je deklarován příkaz pro provedení operace uložení dat.
        public ICommand UlozitCommand { get; set; }

        /// <summary>
        /// V rámci konstruktoru je inicializována editovaná kategorie a je přiřazen příkaz pro uložení.
        /// </summary>
        /// <param name="kategorie">Instance kategorie předaná k úpravě.</param>
        public KategorieEditViewModel(Kategorie kategorie)
        {
            EditovanaKategorie = kategorie;
            UlozitCommand = new RelayCommand(Ulozit);
        }

        /// <summary>
        /// Je provedena logika uložení, která zahrnuje validaci dat a uzavření editačního okna.
        /// </summary>
        /// <param name="parameter">Odkaz na okno předaný z View pro zajištění jeho uzavření.</param>
        private void Ulozit(object parameter)
        {
            // Je prověřeno, zda je vyplněn povinný název kategorie. 
            // V případě prázdného řetězce je uživatel informován o chybě.
            if (string.IsNullOrWhiteSpace(EditovanaKategorie.NazevKategorie))
            {
                MessageBox.Show("Musíte vyplnit název kategorie!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Pokud je předaný parametr platným oknem, je nastaven kladný výsledek dialogu a okno je uzavřeno.
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}