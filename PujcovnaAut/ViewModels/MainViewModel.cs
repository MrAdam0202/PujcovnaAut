using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers;
using PujcovnaAut.Model;
using PujcovnaAut.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

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
                if (VypujckyView != null) VypujckyView.Refresh();
            }
        }

        // --- PŘÍKAZY ---
        public ICommand VratitAutoCommand { get; set; }
        public ICommand NovaVypujckaCommand { get; set; }
        public ICommand UpravitVypujckuCommand { get; set; }

        public ICommand SpravaZakaznikuCommand { get; set; }
        public ICommand SpravaVozidelCommand { get; set; }
        public ICommand SpravaSazebCommand { get; set; }

        public MainViewModel()
        {
            if (Globals.context == null) Globals.Initialize();
            NacistData();

            // 1. NOVÁ VÝPŮJČKA
            NovaVypujckaCommand = new RelayCommand(NovaVypujcka);

            // 2. VRÁCENÍ (Opraveno porovnání na Enum)
            VratitAutoCommand = new RelayCommand(VratitAuto, x =>
                VybranaVypujcka != null && VybranaVypujcka.Stav != StavVypujcky.Ukonceno);

            // 3. ÚPRAVA (Opraveno porovnání na Enum)
            UpravitVypujckuCommand = new RelayCommand(UpravitVypujcku, x =>
                VybranaVypujcka != null && VybranaVypujcka.Stav != StavVypujcky.Ukonceno);

            // 4. NAVIGACE
            SpravaVozidelCommand = new RelayCommand(x => { new SpravaVozidelView().ShowDialog(); NacistData(); });
            SpravaZakaznikuCommand = new RelayCommand(x => { new SpravaZakaznikuView().ShowDialog(); NacistData(); });
            SpravaSazebCommand = new RelayCommand(x => { new SpravaSazebView().ShowDialog(); });
        }

        private void NacistData()
        {
            var vypujcky = Globals.context.Vypujcky
                                  .Include(v => v.Zakaznik)
                                  .Include(v => v.Auto).ThenInclude(a => a.Kategorie)
                                  .Include(v => v.Pojisteni)
                                  .ToList();

            VypujckySource = new ObservableCollection<Vypujcka>(vypujcky);
            VypujckyView = CollectionViewSource.GetDefaultView(VypujckySource);
            VypujckyView.Filter = FilterVypujcek;

            var zakazniciList = Globals.context.Zakaznici.ToList();
            zakazniciList.Insert(0, new Zakaznik { ZakaznikId = -1, Prijmeni = "(Všichni)", Jmeno = "" });
            ZakazniciCol = new ObservableCollection<Zakaznik>(zakazniciList);
            VybranyZakaznikFiltr = ZakazniciCol.First();
        }

        private bool FilterVypujcek(object item)
        {
            if (VybranyZakaznikFiltr == null || VybranyZakaznikFiltr.Prijmeni == "(Všichni)") return true;
            return (item as Vypujcka)?.ZakaznikId == VybranyZakaznikFiltr.ZakaznikId;
        }

        private void NovaVypujcka(object parameter)
        {
            var nova = new Vypujcka();
            nova.Stav = StavVypujcky.Zapujceno; // Použití Enumu
            nova.DatumOd = DateTime.Now;
            nova.DatumDo = DateTime.Now.AddDays(1);

            var vm = new VypujckaEditViewModel(nova);
            var okno = new VypujckaEditView(vm) { Title = "Nová výpůjčka" };

            if (okno.ShowDialog() == true)
            {
                // Výpočet
                int pocetDni = (int)(nova.DatumDo - nova.DatumOd).TotalDays;
                if (pocetDni < 1) pocetDni = 1;
                nova.PocetDni = pocetDni;

                if (nova.Auto != null && nova.Pojisteni != null)
                {
                    if (nova.Auto.Kategorie == null)
                    {
                        var autoZDb = Globals.context.Auta.Include(a => a.Kategorie).FirstOrDefault(a => a.AutoId == nova.AutoId);
                        if (autoZDb != null) nova.Auto.Kategorie = autoZDb.Kategorie;
                    }

                    // Výpočet ceny dle analýzy
                    decimal cenaAuto = nova.Auto.Kategorie.DenniSazba * pocetDni;
                    decimal koeficient = (decimal)nova.Auto.Kategorie.KoeficientPoj;
                    decimal cenaPojisteni = (nova.Pojisteni.CenaZaDen * koeficient) * pocetDni;

                    nova.CenaCelkem = cenaAuto + cenaPojisteni;

                    // Nastavení stavu auta
                    nova.Auto.Stav = StavAuta.Pujcene;
                }

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
            else { Globals.Vratit(); NacistData(); }
        }

        private void VratitAuto(object parameter)
        {
            if (VybranaVypujcka == null) return;
            if (MessageBox.Show($"Vrátit {VybranaVypujcka.Auto?.SPZ}?", "Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                VybranaVypujcka.Stav = StavVypujcky.Ukonceno; // Použití Enumu
                if (VybranaVypujcka.Auto != null) VybranaVypujcka.Auto.Stav = StavAuta.Volne;
                Globals.UlozitData();
                VypujckyView.Refresh();
            }
        }
    }
}