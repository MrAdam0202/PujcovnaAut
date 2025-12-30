using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Helpers; // Pokud máte RelayCommand v Helpers
using DataEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PujcovnaAut.ViewModels
{
    public class VypujckaEditViewModel : BaseVM
    {
        public Vypujcka Vypujcka { get; private set; }

        // --- DATA ---
        public ObservableCollection<AutoVyberItem> AutaVyberCol { get; set; }
        public ObservableCollection<Zakaznik> ZakazniciCol { get; set; }
        public ObservableCollection<Pojisteni> PojisteniCol { get; set; }

        // --- VÝBĚR AUTA ---
        private AutoVyberItem _vybranyItemAuto;
        public AutoVyberItem VybranyItemAuto
        {
            get => _vybranyItemAuto;
            set
            {
                _vybranyItemAuto = value;
                OnPropertyChanged();

                if (Vypujcka != null && value != null)
                {
                    Vypujcka.Auto = value.Auto;
                    Vypujcka.AutoId = value.Auto.AutoId;
                }
                PrepocitatCenu();
            }
        }

        private int _pocetDni;
        public int PocetDni
        {
            get => _pocetDni;
            set { _pocetDni = value; OnPropertyChanged(); }
        }

        private decimal _predbeznaCena;
        public decimal PredbeznaCena
        {
            get => _predbeznaCena;
            set { _predbeznaCena = value; OnPropertyChanged(); }
        }

        private List<Vypujcka> _existujiciVypujcky;

        public VypujckaEditViewModel(Vypujcka vypujcka)
        {
            if (Globals.context == null) Globals.Initialize();

            this.Vypujcka = vypujcka;

            if (Vypujcka.DatumOd == DateTime.MinValue) Vypujcka.DatumOd = DateTime.Now;
            if (Vypujcka.DatumDo == DateTime.MinValue) Vypujcka.DatumDo = DateTime.Now.AddDays(1);

            NacistData();
        }

        private void NacistData()
        {
            _existujiciVypujcky = Globals.context.Vypujcky
                .Where(v => v.Stav != StavVypujcky.Ukonceno && v.VypujckaId != Vypujcka.VypujckaId)
                .ToList();

            var vsechnaAuta = Globals.context.Auta.Include(a => a.Kategorie).ToList();

            AutaVyberCol = new ObservableCollection<AutoVyberItem>();
            foreach (var auto in vsechnaAuta)
            {
                var item = new AutoVyberItem { Auto = auto, JeDostupne = true, Text = auto.PopisAuta };
                AutaVyberCol.Add(item);
            }

            ZakazniciCol = new ObservableCollection<Zakaznik>(Globals.context.Zakaznici.ToList());
            PojisteniCol = new ObservableCollection<Pojisteni>(Globals.context.Pojisteni.ToList());

            AktualizovatDostupnost();

            if (Vypujcka.Auto != null)
            {
                VybranyItemAuto = AutaVyberCol.FirstOrDefault(x => x.Auto.AutoId == Vypujcka.AutoId);
            }

            PrepocitatCenu();
        }

        public void PriZmeneData()
        {
            AktualizovatDostupnost();
            PrepocitatCenu();
            OnPropertyChanged(nameof(Vypujcka));
        }

        private void AktualizovatDostupnost()
        {
            if (AutaVyberCol == null) return;

            foreach (var item in AutaVyberCol)
            {
                bool jeVolne = true;
                string duvod = "";


                // Kontrola dostupnosti jen podle termínu výpůjček
                bool kolize = _existujiciVypujcky.Any(v =>
                    v.AutoId == item.Auto.AutoId &&
                    (v.DatumOd.Date < Vypujcka.DatumDo.Date && Vypujcka.DatumOd.Date < v.DatumDo.Date)
                );

                if (kolize)
                {
                    jeVolne = false;
                    duvod = "(Nedostupné - termín obsazen)";
                }

                // Volitelně: Pokud je auto globálně označené jako "Půjčené" a nemá to vazbu na konkrétní data,
                // můžete to řešit zde, ale logika "kolize" výše je přesnější.

                item.JeDostupne = jeVolne;
                item.Text = $"{item.Auto.PopisAuta} {duvod}";
            }
        }

        private void PrepocitatCenu()
        {
            TimeSpan rozdil = Vypujcka.DatumDo.Date - Vypujcka.DatumOd.Date;
            int dny = (int)rozdil.TotalDays;
            if (dny < 1) dny = 1;

            PocetDni = dny;
            Vypujcka.PocetDni = dny;

            if (VybranyItemAuto?.Auto?.Kategorie == null || Vypujcka.Pojisteni == null)
            {
                PredbeznaCena = 0;
                return;
            }

            decimal cenaAuto = VybranyItemAuto.Auto.Kategorie.DenniSazba * dny;
            // Převedení koeficientu z double na decimal pro výpočet
            decimal koef = (decimal)VybranyItemAuto.Auto.Kategorie.KoeficientPoj;

            decimal cenaPojisteni = (Vypujcka.Pojisteni.CenaZaDen * koef) * dny;

            decimal vyslednaCena = cenaAuto + cenaPojisteni;

            PredbeznaCena = vyslednaCena;
            Vypujcka.CenaCelkem = vyslednaCena;
        }
    }

    public class AutoVyberItem : BaseVM
    {
        public Auto Auto { get; set; }

        private bool _jeDostupne;
        public bool JeDostupne
        {
            get => _jeDostupne;
            set { _jeDostupne = value; OnPropertyChanged(); OnPropertyChanged(nameof(BarvaPisma)); }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public string BarvaPisma => JeDostupne ? "Black" : "Gray";
    }
}