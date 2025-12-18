using PujcovnaAut.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace PujcovnaAut.ViewModels
{
    public class AutoEditViewModel : BaseVM
    {
        public Auto Auto { get; set; }

        // Seznamy pro ComboBoxy (výběr kategorie a stavu)
        public ObservableCollection<Kategorie> KategorieCol { get; set; }
        public ObservableCollection<StavAuta> StavyCol { get; set; }

        public AutoEditViewModel(Auto auto)
        {
            Auto = auto;

            // Načteme kategorie pro výběr
            if (Globals.context != null)
            {
                var kat = Globals.context.Kategorie.ToList();
                KategorieCol = new ObservableCollection<Kategorie>(kat);
            }

            // Naplníme stavy (natvrdo enum hodnoty)
            StavyCol = new ObservableCollection<StavAuta>
            {
                StavAuta.Volne,
                StavAuta.Pujcene,
                StavAuta.Servis,
                StavAuta.Vyrazene
            };
        }
    }
}