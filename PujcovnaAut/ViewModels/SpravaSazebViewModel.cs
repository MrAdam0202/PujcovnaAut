using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers;
using PujcovnaAut.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaAut.ViewModels
{
    public class SpravaSazebViewModel : BaseVM
    {
        // --- DATA PRO KATEGORIE ---
        public ObservableCollection<Kategorie> KategorieCol { get; set; }

        private Kategorie? _vybranaKategorie;
        public Kategorie? VybranaKategorie
        {
            get => _vybranaKategorie;
            set { _vybranaKategorie = value; OnPropertyChanged(); }
        }

        // --- DATA PRO POJIŠTĚNÍ ---
        public ObservableCollection<Pojisteni> PojisteniCol { get; set; }

        private Pojisteni? _vybranePojisteni;
        public Pojisteni? VybranePojisteni
        {
            get => _vybranePojisteni;
            set { _vybranePojisteni = value; OnPropertyChanged(); }
        }

        // --- PŘÍKAZY ---
        public ICommand PridatKategoriiCommand { get; set; }
        public ICommand SmazatKategoriiCommand { get; set; }

        public ICommand PridatPojisteniCommand { get; set; }
        public ICommand SmazatPojisteniCommand { get; set; }

        public ICommand UlozitCommand { get; set; }

        public SpravaSazebViewModel()
        {
            if (Globals.context == null) Globals.Initialize();

            NacistData();
            InicializovatPrikazy();
        }

        private void NacistData()
        {
            // Načtení seznamů z DB
            KategorieCol = new ObservableCollection<Kategorie>(Globals.context.Kategorie.ToList());

            // Pokud tabulka Pojištění existuje
            if (Globals.context.Pojisteni != null)
                PojisteniCol = new ObservableCollection<Pojisteni>(Globals.context.Pojisteni.ToList());
            else
                PojisteniCol = new ObservableCollection<Pojisteni>();
        }

        private void InicializovatPrikazy()
        {
            // 1. KATEGORIE
            PridatKategoriiCommand = new RelayCommand(x =>
            {
                var nova = new Kategorie { NazevKategorie = "Nová kategorie", DenniSazba = 1000 };
                Globals.context.Kategorie.Add(nova);
                KategorieCol.Add(nova);
                VybranaKategorie = nova;
            });

            SmazatKategoriiCommand = new RelayCommand(x =>
            {
                if (VybranaKategorie != null)
                {
                    if (MessageBox.Show("Smazat kategorii? (Pozor, pokud ji používají auta, smazání selže)", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Globals.context.Kategorie.Remove(VybranaKategorie);
                            Globals.UlozitData(); // Zkusíme uložit hned, abychom zjistili vazby
                            KategorieCol.Remove(VybranaKategorie);
                        }
                        catch
                        {
                            MessageBox.Show("Nelze smazat kategorii, protože je přiřazena k autům!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                            Globals.Vratit(); // Vratit změny v kontextu
                            NacistData(); // Obnovit data
                        }
                    }
                }
            }, x => VybranaKategorie != null);

            // 2. POJIŠTĚNÍ
            PridatPojisteniCommand = new RelayCommand(x =>
            {
                var nove = new Pojisteni { NazevPlanu = "Nové pojištění", CenaZaDen = 100 };
                Globals.context.Pojisteni.Add(nove);
                PojisteniCol.Add(nove);
                VybranePojisteni = nove;
            });

            SmazatPojisteniCommand = new RelayCommand(x =>
            {
                if (VybranePojisteni != null)
                {
                    if (MessageBox.Show("Smazat typ pojištění?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Globals.context.Pojisteni.Remove(VybranePojisteni);
                        PojisteniCol.Remove(VybranePojisteni);
                    }
                }
            }, x => VybranePojisteni != null);

            // 3. SPOLEČNÉ ULOŽENÍ
            UlozitCommand = new RelayCommand(x =>
            {
                Globals.UlozitData();
                MessageBox.Show("Změny uloženy.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}