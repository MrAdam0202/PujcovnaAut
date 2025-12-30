using DataEntity;
using System.Collections.ObjectModel;
using System.Linq;

namespace PujcovnaAut.ViewModels
{
    public class AutoEditViewModel : BaseVM
    {
        public Auto Auto { get; set; }

        public ObservableCollection<Kategorie> KategorieCol { get; set; }
        public ObservableCollection<StavAuta> StavyCol { get; set; }

        public AutoEditViewModel(Auto auto)
        {
            Auto = auto;

            if (Globals.context != null)
            {
                var kat = Globals.context.Kategorie.ToList();
                KategorieCol = new ObservableCollection<Kategorie>(kat);
            }

            // OPRAVA: Odstraněny neexistující stavy Servis a Vyrazene
            StavyCol = new ObservableCollection<StavAuta>
            {
                StavAuta.Volne,
                StavAuta.Pujcene
            };
        }
    }
}