using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers;
using DataEntity;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaAut.ViewModels
{
    /// <summary>
    /// ViewModel určený pro správu agendy zákazníků. 
    /// Jsou zde implementovány operace pro prohlížení, zakládání, modifikaci a odstraňování klientských záznamů.
    /// </summary>
    public class SpravaZakaznikuViewModel : BaseVM
    {
        // --- DATOVÉ ČLENY ---

        // Pozorovatelná kolekce zákazníků je inicializována pro potřeby vazby na datový pohled (DataGrid).
        public ObservableCollection<Zakaznik> ZakazniciCol { get; set; } = new ObservableCollection<Zakaznik>();

        private Zakaznik? _vybranyZakaznik;
        /// <summary>
        /// Je spravována instance aktuálně zvoleného zákazníka. 
        /// Při změně výběru je vyvolána notifikace pro aktualizaci editačních prvků v uživatelském rozhraní.
        /// </summary>
        public Zakaznik? VybranyZakaznik
        {
            get => _vybranyZakaznik;
            set { _vybranyZakaznik = value; OnPropertyChanged(); }
        }

        // --- PŘÍKAZY (Commands) ---
        public ICommand NovyZaznamCommand { get; set; }
        public ICommand SmazatZaznamCommand { get; set; }
        public ICommand UlozitCommand { get; set; }
        public ICommand StornoCommand { get; set; }

        /// <summary>
        /// V rámci konstruktoru je prověřena inicializace databázového kontextu a následně spuštěno načtení dat a definice příkazů.
        /// </summary>
        public SpravaZakaznikuViewModel()
        {
            if (Globals.context == null) Globals.Initialize();
            NacistData();
            InicializovatPrikazy();
        }

        /// <summary>
        /// Je provedeno načtení dat z databázového zdroje a jejich transformace do kolekce určené pro zobrazení.
        /// </summary>
        private void NacistData()
        {
            var data = Globals.context.Zakaznici.ToList();
            ZakazniciCol = new ObservableCollection<Zakaznik>(data);
        }

        /// <summary>
        /// Je provedena definice a inicializace logiky jednotlivých příkazů.
        /// </summary>
        private void InicializovatPrikazy()
        {
            // 1. PŘÍKAZ PRO NOVÝ ZÁZNAM
            // Je vytvořena nová instance zákazníka s předdefinovaným datem narození (věk 18 let) pro usnadnění obsluhy kalendáře.
            NovyZaznamCommand = new RelayCommand(x =>
            {
                var novy = new Zakaznik();
                novy.DatumNarozeni = System.DateTime.Now.AddYears(-18);

                Globals.context.Zakaznici.Add(novy);
                ZakazniciCol.Add(novy);
                VybranyZakaznik = novy;
            });

            // 2. PŘÍKAZ PRO SMAZÁNÍ ZÁZNAMU
            // Je vyžadováno potvrzení operace uživatelem. Po schválení je záznam odstraněn z kontextu i kolekce a změny jsou uloženy.
            SmazatZaznamCommand = new RelayCommand(x =>
            {
                if (VybranyZakaznik != null)
                {
                    if (MessageBox.Show($"Opravdu smazat zákazníka {VybranyZakaznik.Prijmeni}?", "Potvrzení smazání", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Globals.context.Zakaznici.Remove(VybranyZakaznik);
                        Globals.UlozitData();
                        ZakazniciCol.Remove(VybranyZakaznik);
                        VybranyZakaznik = null;
                    }
                }
            }, x => VybranyZakaznik != null);

            // 3. PŘÍKAZ PRO ULOŽENÍ ZMĚN
            // Je vyvolána metoda pro trvalé uložení změn v databázi a následně je provedena aktualizace zobrazení v kolekci.
            UlozitCommand = new RelayCommand(x =>
            {
                if (VybranyZakaznik != null)
                {
                    Globals.UlozitData();

                    var index = ZakazniciCol.IndexOf(VybranyZakaznik);
                    if (index != -1) ZakazniciCol[index] = VybranyZakaznik;

                    MessageBox.Show("Změny byly úspěšně uloženy.", "Informace", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });

            // 4. PŘÍKAZ PRO ZRUŠENÍ ZMĚN (STORNO)
            // Neuložené změny v databázovém kontextu jsou vráceny zpět a data jsou znovu synchronizována s databází.
            StornoCommand = new RelayCommand(x =>
            {
                Globals.Vratit();
                NacistData();
                VybranyZakaznik = null;
            });
        }
    }
}