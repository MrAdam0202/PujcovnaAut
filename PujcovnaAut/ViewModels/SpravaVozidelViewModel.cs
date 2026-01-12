using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers;
using DataEntity;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DataEntity.Models.Enums;

namespace PujcovnaAut.ViewModels
{
    /// <summary>
    /// ViewModel pro správu vozového parku.
    /// Jsou zde definovány kolekce pro zobrazení vozidel a kategorií a příkazy pro manipulaci s daty.
    /// </summary>
    public class SpravaVozidelViewModel : BaseVM
    {
        // --- DATA ---
        // Kolekce vozidel je využívána pro zobrazení seznamu v uživatelském rozhraní.
        public ObservableCollection<Auto> AutaCol { get; set; }

        // Kolekce kategorií je využívána pro přiřazení vozidla do konkrétní cenové skupiny.
        public ObservableCollection<Kategorie> KategorieCol { get; set; }

        private Auto _vybraneAuto;
        /// <summary>
        /// Je spravována instance aktuálně vybraného vozidla. 
        /// Při změně výběru je informováno uživatelské rozhraní pro aktualizaci polí v editačním formuláři.
        /// </summary>
        public Auto VybraneAuto
        {
            get => _vybraneAuto;
            set { _vybraneAuto = value; OnPropertyChanged(); }
        }

        // --- PŘÍKAZY (Commands) ---
        public ICommand NovyVuzCommand { get; set; }
        public ICommand SmazatVuzCommand { get; set; }
        public ICommand UlozitCommand { get; set; }
        public ICommand NacistDataCommand { get; set; }

        public SpravaVozidelViewModel()
        {
            // Je ověřena existence databázového kontextu, případně je provedena jeho inicializace.
            if (Globals.context == null) Globals.Initialize();

            NacistData();
            InicializovatPrikazy();
        }

        /// <summary>
        /// Je provedeno načtení dat z databázového kontextu. 
        /// Jsou načtena všechna vozidla včetně jejich vazeb na kategorie a kompletní seznam kategorií.
        /// </summary>
        private void NacistData()
        {
            AutaCol = new ObservableCollection<Auto>(Globals.context.Auta.Include(a => a.Kategorie).ToList());
            KategorieCol = new ObservableCollection<Kategorie>(Globals.context.Kategorie.ToList());
        }

        /// <summary>
        /// Je provedena definice a inicializace všech příkazů pro ovládání prvků v uživatelském rozhraní.
        /// </summary>
        private void InicializovatPrikazy()
        {
            // Je definován příkaz pro zrušení výběru a obnovení dat ze serveru.
            NacistDataCommand = new RelayCommand(x =>
            {
                VybraneAuto = null;
                NacistData();
            });

            // Je definován příkaz pro vytvoření nového záznamu vozidla.
            NovyVuzCommand = new RelayCommand(x =>
            {
                // Je vytvořena nová instance vozidla s výchozími hodnotami.
                var nove = new Auto
                {
                    SPZ = "",
                    Znacka = "",
                    Model = "",
                    // Stav nově vloženého vozidla je automaticky nastaven jako volný.
                    Stav = StavAuta.Volne
                };

                // Je provedeno automatické přiřazení první dostupné kategorie pro zajištění validity dat.
                if (KategorieCol.Any())
                {
                    nove.Kategorie = KategorieCol.First();
                    nove.KategorieId = nove.Kategorie.KategorieId;
                }

                // Nový záznam je přidán do databázového kontextu i do zobrazované kolekce.
                Globals.context.Auta.Add(nove);
                AutaCol.Add(nove);
                VybraneAuto = nove;
            });

            // Je definován příkaz pro odstranění vybraného vozidla.
            SmazatVuzCommand = new RelayCommand(x =>
            {
                if (VybraneAuto != null)
                {
                    // Je vyžadováno potvrzení uživatele před definitivním odstraněním záznamu.
                    if (MessageBox.Show($"Opravdu smazat vozidlo {VybraneAuto.SPZ}?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            // Je proveden pokus o odstranění z kontextu a uložení změn.
                            Globals.context.Auta.Remove(VybraneAuto);
                            Globals.UlozitData();
                            AutaCol.Remove(VybraneAuto);
                            VybraneAuto = null;
                        }
                        catch
                        {
                            // V případě existence cizích klíčů (např. v historii výpůjček) je smazání zamítnuto a proveden návrat změn.
                            MessageBox.Show("Vůz nelze smazat, pravděpodobně má v historii výpůjčky.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                            Globals.Vratit();
                        }
                    }
                }
            }, x => VybraneAuto != null);

            // Je definován příkaz pro trvalé uložení všech provedených změn do databáze.
            UlozitCommand = new RelayCommand(x =>
            {
                Globals.UlozitData();
                MessageBox.Show("Změny uloženy.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}