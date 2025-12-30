using PropertyChanged;
using PujcovnaAut.Helpers;
using DataEntity;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaAut.ViewModels
{
    /// <summary>
    /// ViewModel určený pro editační rozhraní osobních údajů zákazníků.
    /// Jsou zde implementovány mechanismy pro předávání dat mezi modelem a pohledem a obsluha ukončení dialogu.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class ZakaznikEditViewModel : BaseVM
    {
        // Je definována instance zákazníka, která je předmětem probíhající editace.
        public Zakaznik EditovanyZakaznik { get; set; }

        // Je deklarován příkaz pro potvrzení a uložení provedených změn v datech.
        public ICommand UlozitCommand { get; set; }

        /// <summary>
        /// V rámci konstruktoru je inicializován upravovaný objekt a je definován příkaz pro uložení změn.
        /// </summary>
        /// <param name="zakaznik">Instance zákazníka předaná k modifikaci.</param>
        public ZakaznikEditViewModel(Zakaznik zakaznik)
        {
            EditovanyZakaznik = zakaznik;
            UlozitCommand = new RelayCommand(Ulozit, MuzeUlozit);
        }

        /// <summary>
        /// Je prověřována možnost uložení dat. V aktuální implementaci je validace ponechána 
        /// na úrovni grafického rozhraní (Bindingy) nebo globálních validačních metod.
        /// </summary>
        /// <param name="parameter">Doplňující parametry příkazu.</param>
        /// <returns>Logická hodnota indikující, zda lze operaci uložení provést.</returns>
        private bool MuzeUlozit(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Je provedeno ukončení editačního dialogu. Pokud je jako parametr předáno platné okno, 
        /// je nastaven kladný výsledek dialogu a okno je uzavřeno.
        /// </summary>
        /// <param name="parameter">Odkaz na grafické okno předaný z View.</param>
        private void Ulozit(object parameter)
        {
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}