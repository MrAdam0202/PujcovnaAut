using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers;
using DataEntity;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaAut.ViewModels
{
    public class SpravaVozidelViewModel : BaseVM
    {
        // --- DATA ---
        public ObservableCollection<Auto> AutaCol { get; set; }
        public ObservableCollection<Kategorie> KategorieCol { get; set; }

        private Auto _vybraneAuto;
        public Auto VybraneAuto
        {
            get => _vybraneAuto;
            set { _vybraneAuto = value; OnPropertyChanged(); }
        }

        // --- PŘÍKAZY ---
        public ICommand NovyVuzCommand { get; set; }
        public ICommand SmazatVuzCommand { get; set; }
        public ICommand UlozitCommand { get; set; }
        public ICommand NacistDataCommand { get; set; } // Pro tlačítko "Zrušit výběr"

        public SpravaVozidelViewModel()
        {
            if (Globals.context == null) Globals.Initialize();

            NacistData();
            InicializovatPrikazy();
        }

        private void NacistData()
        {
            // Načteme auta i s kategorií
            AutaCol = new ObservableCollection<Auto>(Globals.context.Auta.Include(a => a.Kategorie).ToList());
            KategorieCol = new ObservableCollection<Kategorie>(Globals.context.Kategorie.ToList());
        }

        private void InicializovatPrikazy()
        {
            // Tlačítko pro zrušení výběru (čistý formulář)
            NacistDataCommand = new RelayCommand(x =>
            {
                VybraneAuto = null;
                NacistData();
            });

            NovyVuzCommand = new RelayCommand(x =>
            {
                var nove = new Auto
                {
                    SPZ = "",
                    Znacka = "",
                    Model = "",
                    // DŮLEŽITÉ: Nové auto je vždy Volné. O zbytek se starají výpůjčky.
                    Stav = StavAuta.Volne
                };

                // Předvyplnění kategorie, aby to nepadalo
                if (KategorieCol.Any())
                {
                    nove.Kategorie = KategorieCol.First();
                    nove.KategorieId = nove.Kategorie.KategorieId;
                }

                Globals.context.Auta.Add(nove);
                AutaCol.Add(nove);
                VybraneAuto = nove; // Přepneme se na něj
            });

            SmazatVuzCommand = new RelayCommand(x =>
            {
                if (VybraneAuto != null)
                {
                    if (MessageBox.Show($"Opravdu smazat vozidlo {VybraneAuto.SPZ}?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Globals.context.Auta.Remove(VybraneAuto);
                            Globals.UlozitData();
                            AutaCol.Remove(VybraneAuto);
                            VybraneAuto = null;
                        }
                        catch
                        {
                            MessageBox.Show("Vůz nelze smazat, pravděpodobně má v historii výpůjčky.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                            // V případě chyby vrátíme změny (Entity Framework tracking)
                            Globals.Vratit();
                        }
                    }
                }
            }, x => VybraneAuto != null);

            UlozitCommand = new RelayCommand(x =>
            {
                Globals.UlozitData();
                MessageBox.Show("Změny uloženy.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}