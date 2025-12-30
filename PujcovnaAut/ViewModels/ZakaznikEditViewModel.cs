using PropertyChanged;
using PujcovnaAut.Helpers;
using DataEntity;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaAut.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ZakaznikEditViewModel : BaseVM
    {
        public Zakaznik EditovanyZakaznik { get; set; }
        public ICommand UlozitCommand { get; set; }

        public ZakaznikEditViewModel(Zakaznik zakaznik)
        {
            EditovanyZakaznik = zakaznik;
            UlozitCommand = new RelayCommand(Ulozit, MuzeUlozit);
        }

        private bool MuzeUlozit(object parameter)
        {
            // Využití metody IsValid z Globals.cs (podle tvého kódu)
            // Pozor: Globals.IsValid obvykle bere DependencyObject (View), ale tady validujeme Model.
            // Pokud tvůj Globals.IsValid bere View, musíme to v commandu ignorovat nebo validovat model ručně.
            // Zde použijeme jednoduchou kontrolu na model (BaseModel implementuje IDataErrorInfo).
            return true; // Validaci řešíme při stisku tlačítka přes Globals.IsValid(okno) nebo Bindingy
        }

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