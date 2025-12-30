using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers;
using DataEntity;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaAut.ViewModels
{
    public class SpravaZakaznikuViewModel : BaseVM
    {
        // --- DATA ---
        // Inicializujeme kolekci, abychom se vyhli warningům
        public ObservableCollection<Zakaznik> ZakazniciCol { get; set; } = new ObservableCollection<Zakaznik>();

        // --- VÝBĚR ---
        private Zakaznik? _vybranyZakaznik;
        public Zakaznik? VybranyZakaznik
        {
            get => _vybranyZakaznik;
            set { _vybranyZakaznik = value; OnPropertyChanged(); }
        }

        // --- PŘÍKAZY ---
        public ICommand NovyZaznamCommand { get; set; }
        public ICommand SmazatZaznamCommand { get; set; }
        public ICommand UlozitCommand { get; set; }
        public ICommand StornoCommand { get; set; }

        public SpravaZakaznikuViewModel()
        {
            if (Globals.context == null) Globals.Initialize();
            NacistData();
            InicializovatPrikazy();
        }

        private void NacistData()
        {
            // Načtení dat z DB
            var data = Globals.context.Zakaznici.ToList();
            ZakazniciCol = new ObservableCollection<Zakaznik>(data);
        }

        private void InicializovatPrikazy()
        {
            // 1. NOVÝ
            NovyZaznamCommand = new RelayCommand(x =>
            {
                var novy = new Zakaznik();
                // Nastavíme nějaké defaultní datum narození, aby kalendář neukazoval rok 0001
                novy.DatumNarozeni = System.DateTime.Now.AddYears(-18);

                Globals.context.Zakaznici.Add(novy);
                ZakazniciCol.Add(novy);
                VybranyZakaznik = novy;
            });

            // 2. SMAZAT
            SmazatZaznamCommand = new RelayCommand(x =>
            {
                if (VybranyZakaznik != null)
                {
                    if (MessageBox.Show($"Smazat zákazníka {VybranyZakaznik.Prijmeni}?", "Dotaz", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Globals.context.Zakaznici.Remove(VybranyZakaznik);
                        Globals.UlozitData();
                        ZakazniciCol.Remove(VybranyZakaznik);
                        VybranyZakaznik = null;
                    }
                }
            }, x => VybranyZakaznik != null);

            // 3. ULOŽIT
            UlozitCommand = new RelayCommand(x =>
            {
                if (VybranyZakaznik != null)
                {
                    Globals.UlozitData();
                    // Refresh pro grid (pokud se změnilo jméno)
                    var index = ZakazniciCol.IndexOf(VybranyZakaznik);
                    if (index != -1) ZakazniciCol[index] = VybranyZakaznik;

                    MessageBox.Show("Uloženo.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });

            // 4. STORNO
            StornoCommand = new RelayCommand(x =>
            {
                Globals.Vratit();
                NacistData();
                VybranyZakaznik = null;
            });
        }
    }
}