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
    /// ViewModel určený pro správu číselníků cenových kategorií a pojistných plánů.
    /// Jsou zde definovány operace pro vytváření, odstraňování a editaci sazeb.
    /// </summary>
    public class SpravaSazebViewModel : BaseVM
    {
        // --- SEKCE KATEGORIÍ ---
        // Kolekce je využívána pro zobrazení dostupných cenových kategorií vozidel.
        public ObservableCollection<Kategorie> KategorieCol { get; set; }

        private Kategorie? _vybranaKategorie;
        /// <summary>
        /// Je spravována instance aktuálně zvolené kategorie v seznamu.
        /// </summary>
        public Kategorie? VybranaKategorie
        {
            get => _vybranaKategorie;
            set { _vybranaKategorie = value; OnPropertyChanged(); }
        }

        // --- SEKCE POJIŠTĚNÍ ---
        // Kolekce je využívána pro správu a zobrazení nabízených typů pojištění.
        public ObservableCollection<Pojisteni> PojisteniCol { get; set; }

        private Pojisteni? _vybranePojisteni;
        /// <summary>
        /// Je spravována instance aktuálně zvoleného typu pojištění.
        /// </summary>
        public Pojisteni? VybranePojisteni
        {
            get => _vybranePojisteni;
            set { _vybranePojisteni = value; OnPropertyChanged(); }
        }

        // --- PŘÍKAZY (Commands) ---
        public ICommand PridatKategoriiCommand { get; set; }
        public ICommand SmazatKategoriiCommand { get; set; }

        public ICommand PridatPojisteniCommand { get; set; }
        public ICommand SmazatPojisteniCommand { get; set; }

        public ICommand UlozitCommand { get; set; }

        /// <summary>
        /// V rámci konstruktoru je zajištěna inicializace datového kontextu a příkazů.
        /// </summary>
        public SpravaSazebViewModel()
        {
            if (Globals.context == null) Globals.Initialize();

            NacistData();
            InicializovatPrikazy();
        }

        /// <summary>
        /// Je provedeno načtení dat z databáze do lokálních kolekcí pro potřeby uživatelského rozhraní.
        /// </summary>
        private void NacistData()
        {
            KategorieCol = new ObservableCollection<Kategorie>(Globals.context.Kategorie.ToList());

            // Je prověřena existence tabulky pojištění v kontextu a následně naplněna příslušná kolekce.
            if (Globals.context.Pojisteni != null)
                PojisteniCol = new ObservableCollection<Pojisteni>(Globals.context.Pojisteni.ToList());
            else
                PojisteniCol = new ObservableCollection<Pojisteni>();
        }

        /// <summary>
        /// Jsou definovány logické postupy pro jednotlivé operace nad daty.
        /// </summary>
        private void InicializovatPrikazy()
        {
            // --- OPERACE NAD KATEGORIEMI ---
            // Je definován postup pro vytvoření nové cenové kategorie s výchozími parametry.
            PridatKategoriiCommand = new RelayCommand(x =>
            {
                var nova = new Kategorie { NazevKategorie = "Nová kategorie", DenniSazba = 1000 };
                Globals.context.Kategorie.Add(nova);
                KategorieCol.Add(nova);
                VybranaKategorie = nova;
            });

            // Je definován postup pro odstranění kategorie, přičemž je prověřována integrita databáze.
            SmazatKategoriiCommand = new RelayCommand(x =>
            {
                if (VybranaKategorie != null)
                {
                    // Před odstraněním je vyžadováno potvrzení operace uživatelem.
                    if (MessageBox.Show("Smazat vybranou kategorii?", "Potvrzení smazání", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            // Pokus o smazání je doprovázen okamžitým uložením pro detekci případných vazeb na vozidla.
                            Globals.context.Kategorie.Remove(VybranaKategorie);
                            Globals.UlozitData();
                            KategorieCol.Remove(VybranaKategorie);
                        }
                        catch
                        {
                            // Pokud je kategorie provázána s existujícími vozidly, je operace zamítnuta a změny jsou vráceny.
                            MessageBox.Show("Kategorii nelze odstranit, protože je přiřazena k vozidlům v evidenci!", "Chyba integrity", MessageBoxButton.OK, MessageBoxImage.Error);
                            Globals.Vratit();
                            NacistData();
                        }
                    }
                }
            }, x => VybranaKategorie != null);

            // --- OPERACE NAD POJIŠTĚNÍM ---
            // Je definován postup pro přidání nového pojistného plánu.
            PridatPojisteniCommand = new RelayCommand(x =>
            {
                var nove = new Pojisteni { NazevPlanu = "Nové pojištění", CenaZaDen = 100 };
                Globals.context.Pojisteni.Add(nove);
                PojisteniCol.Add(nove);
                VybranePojisteni = nove;
            });

            // Je definován postup pro smazání vybraného typu pojištění.
            SmazatPojisteniCommand = new RelayCommand(x =>
            {
                if (VybranePojisteni != null)
                {
                    if (MessageBox.Show("Smazat vybraný typ pojištění?", "Potvrzení smazání", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Globals.context.Pojisteni.Remove(VybranePojisteni);
                        PojisteniCol.Remove(VybranePojisteni);
                        // Zde je předpokládáno trvalé uložení až při stisku hlavního tlačítka Uložit.
                    }
                }
            }, x => VybranePojisteni != null);

            // --- SPOLEČNÉ OPERACE ---
            // Je definován příkaz pro hromadné uložení všech provedených změn v obou číselnících.
            UlozitCommand = new RelayCommand(x =>
            {
                Globals.UlozitData();
                MessageBox.Show("Veškeré změny v sazebníku byly úspěšně uloženy.", "Informace", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}