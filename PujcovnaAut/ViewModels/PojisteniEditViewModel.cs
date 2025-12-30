using System.Windows;
using System.Windows.Input;
using PujcovnaAut.Helpers;
using DataEntity;

namespace PujcovnaAut.ViewModels
{
    /// <summary>
    /// ViewModel určený pro editační rozhraní pojistných plánů.
    /// Jsou zde implementovány mechanismy pro úpravu parametrů pojištění a validaci vstupů.
    /// </summary>
    public class PojisteniEditViewModel : BaseVM
    {
        // Je definována instance pojištění, která je aktuálně předmětem úprav.
        public Pojisteni EditovanePojisteni { get; set; }

        // Je deklarován příkaz pro potvrzení a uložení provedených změn.
        public ICommand UlozitCommand { get; set; }

        /// <summary>
        /// V rámci konstruktoru je inicializována upravovaná instance a je přiřazen příkaz pro uložení.
        /// </summary>
        /// <param name="pojisteni">Instance pojištění předaná k editaci.</param>
        public PojisteniEditViewModel(Pojisteni pojisteni)
        {
            EditovanePojisteni = pojisteni;
            UlozitCommand = new RelayCommand(Ulozit);
        }

        /// <summary>
        /// Je provedeno zpracování požadavku na uložení, které zahrnuje kontrolu dat a uzavření okna.
        /// </summary>
        /// <param name="parameter">Odkaz na grafické okno předaný z View pro zajištění jeho uzavření.</param>
        private void Ulozit(object parameter)
        {
            // Je prověřeno, zda byl vyplněn povinný název pojistného plánu.
            // Pokud je řetězec prázdný, je uživatel informován prostřednictvím chybového hlášení.
            if (string.IsNullOrWhiteSpace(EditovanePojisteni.NazevPlanu))
            {
                MessageBox.Show("Musíte vyplnit název plánu!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Pokud je jako parametr přijato platné okno, je nastaven kladný výsledek dialogu a okno je uzavřeno.
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}