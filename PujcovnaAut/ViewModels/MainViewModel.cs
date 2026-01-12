using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers;
using DataEntity;
using PujcovnaAut.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DataEntity.Models.Enums;

namespace PujcovnaAut.ViewModels
{
    public class MainViewModel : BaseVM
    {
        // --- DATA ---
        public ObservableCollection<Vypujcka> VypujckySource { get; set; } = new ObservableCollection<Vypujcka>();
        public ICollectionView VypujckyView { get; set; }

        public ObservableCollection<Zakaznik> ZakazniciCol { get; set; } = new ObservableCollection<Zakaznik>();

        private Vypujcka _vybranaVypujcka;
        public Vypujcka VybranaVypujcka
        {
            get => _vybranaVypujcka;
            set { _vybranaVypujcka = value; OnPropertyChanged(); }
        }

        private Zakaznik _vybranyZakaznikFiltr;
        public Zakaznik VybranyZakaznikFiltr
        {
            get => _vybranyZakaznikFiltr;
            set
            {
                _vybranyZakaznikFiltr = value;
                OnPropertyChanged();
                // Při změně filtru je obnoven pohled na data.
                if (VypujckyView != null) VypujckyView.Refresh();
            }
        }

        // --- PŘÍKAZY (Commands) ---
        public ICommand VratitAutoCommand { get; set; }
        public ICommand NovaVypujckaCommand { get; set; }
        public ICommand UpravitVypujckuCommand { get; set; }

        public ICommand SpravaZakaznikuCommand { get; set; }
        public ICommand SpravaVozidelCommand { get; set; }
        public ICommand SpravaSazebCommand { get; set; }

        public MainViewModel()
        {
            // Je zajištěna inicializace kontextu databáze.
            if (Globals.context == null) Globals.Initialize();
            NacistData();

            // 1. Příkaz pro novou výpůjčku.
            NovaVypujckaCommand = new RelayCommand(NovaVypujcka);

            // 2. Příkaz pro vrácení auta. Je povolen pouze pokud je vybrána výpůjčka a není již ukončena.
            VratitAutoCommand = new RelayCommand(VratitAuto, x =>
                VybranaVypujcka != null && VybranaVypujcka.Stav != StavVypujcky.Ukonceno);

            // 3. Příkaz pro úpravu výpůjčky.
            UpravitVypujckuCommand = new RelayCommand(UpravitVypujcku, x =>
                VybranaVypujcka != null && VybranaVypujcka.Stav != StavVypujcky.Ukonceno);

            // 4. Příkazy pro navigaci do ostatních oken správy.
            SpravaVozidelCommand = new RelayCommand(x => { new SpravaVozidelView().ShowDialog(); NacistData(); });
            SpravaZakaznikuCommand = new RelayCommand(x => { new SpravaZakaznikuView().ShowDialog(); NacistData(); });
            SpravaSazebCommand = new RelayCommand(x => { new SpravaSazebView().ShowDialog(); });
        }

        private void NacistData()
        {
            // Jsou načtena data výpůjček včetně vazebných tabulek (Eager Loading).
            var vypujcky = Globals.context.Vypujcky
                                          .Include(v => v.Zakaznik)
                                          .Include(v => v.Auto).ThenInclude(a => a.Kategorie)
                                          .Include(v => v.Pojisteni)
                                          .ToList();

            VypujckySource = new ObservableCollection<Vypujcka>(vypujcky);
            VypujckyView = CollectionViewSource.GetDefaultView(VypujckySource);
            // Je nastaven filtr pro zobrazení.
            VypujckyView.Filter = FilterVypujcek;

            // Jsou načteni zákazníci pro filtrovací menu a je přidána položka "(Všichni)".
            var zakazniciList = Globals.context.Zakaznici.ToList();
            zakazniciList.Insert(0, new Zakaznik { ZakaznikId = -1, Prijmeni = "(Všichni)", Jmeno = "" });
            ZakazniciCol = new ObservableCollection<Zakaznik>(zakazniciList);
            VybranyZakaznikFiltr = ZakazniciCol.First();
        }

        private bool FilterVypujcek(object item)
        {
            // Pokud není vybrán konkrétní zákazník, jsou zobrazeny všechny záznamy.
            if (VybranyZakaznikFiltr == null || VybranyZakaznikFiltr.Prijmeni == "(Všichni)") return true;
            return (item as Vypujcka)?.ZakaznikId == VybranyZakaznikFiltr.ZakaznikId;
        }

        private void NovaVypujcka(object parameter)
        {
            var nova = new Vypujcka();
            nova.Stav = StavVypujcky.Zapujceno;
            nova.DatumOd = DateTime.Now;
            nova.DatumDo = DateTime.Now.AddDays(1);

            var vm = new VypujckaEditViewModel(nova);
            var okno = new VypujckaEditView(vm) { Title = "Nová výpůjčka" };

            // Pokud uživatel potvrdí dialog (OK).
            if (okno.ShowDialog() == true)
            {
                // Je vypočítán počet dní (minimálně 1 den).
                int pocetDni = (int)(nova.DatumDo - nova.DatumOd).TotalDays;
                if (pocetDni < 1) pocetDni = 1;
                nova.PocetDni = pocetDni;

                if (nova.Auto != null && nova.Pojisteni != null)
                {
                    // Je načtena kategorie vozu z databáze pro správný výpočet ceny.
                    if (nova.Auto.Kategorie == null)
                    {
                        var autoZDb = Globals.context.Auta.Include(a => a.Kategorie).FirstOrDefault(a => a.AutoId == nova.AutoId);
                        if (autoZDb != null) nova.Auto.Kategorie = autoZDb.Kategorie;
                    }

                    // Výpočet celkové ceny: (Sazba Auta + (Cena Pojištění * Koeficient)) * Počet Dní
                    decimal cenaAuto = nova.Auto.Kategorie.DenniSazba * pocetDni;
                    decimal koeficient = (decimal)nova.Auto.Kategorie.KoeficientPoj;
                    decimal cenaPojisteni = (nova.Pojisteni.CenaZaDen * koeficient) * pocetDni;

                    nova.CenaCelkem = cenaAuto + cenaPojisteni;

                    // Stav vozidla je změněn na Půjčené.
                    nova.Auto.Stav = StavAuta.Pujcene;
                }

                // Nová výpůjčka je přidána do kontextu a uložena.
                Globals.context.Vypujcky.Add(nova);
                Globals.UlozitData();
                VypujckySource.Add(nova);
                VypujckyView.Refresh();
            }
        }

        private void UpravitVypujcku(object parameter)
        {
            if (VybranaVypujcka == null) return;

            var vm = new VypujckaEditViewModel(VybranaVypujcka);
            if (new VypujckaEditView(vm) { Title = "Upravit" }.ShowDialog() == true)
            {
                Globals.UlozitData();
                VypujckyView.Refresh();
            }
            else
            {
                // Pokud je akce zrušena, změny jsou vráceny zpět.
                Globals.Vratit();
                NacistData();
            }
        }

        private void VratitAuto(object parameter)
        {
            if (VybranaVypujcka == null) return;

            // Je sestaven informační text pro potvrzovací dialog.
            string zprava = $"Chystáte se ukončit výpůjčku. Jsou údaje správné?\n\n" +
                            $"Vozidlo: {VybranaVypujcka.Auto.Znacka} {VybranaVypujcka.Auto.Model}\n" +
                            $"SPZ: {VybranaVypujcka.Auto.SPZ}\n" +
                            $"Zákazník: {VybranaVypujcka.Zakaznik.Jmeno} {VybranaVypujcka.Zakaznik.Prijmeni}\n" +
                            $"Plánované vrácení: {VybranaVypujcka.DatumDo:dd.MM.yyyy}";

            // Je zobrazeno potvrzovací okno.
            if (MessageBox.Show(zprava, "Potvrzení vrácení vozu", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Stav výpůjčky je změněn na Ukončeno.
                VybranaVypujcka.Stav = StavVypujcky.Ukonceno;

                // Stav auta je změněn zpět na Volné.
                if (VybranaVypujcka.Auto != null)
                {
                    VybranaVypujcka.Auto.Stav = StavAuta.Volne;
                }

                Globals.UlozitData();
                VypujckyView.Refresh();
            }
        }
    }
}