using System.Windows;
using PujcovnaAut.ViewModels; // Nutné pro přístup k ViewModelu

namespace PujcovnaAut
{
    public partial class KategorieEditView : Window
    {
        // Upravíme konstruktor, aby přijímal ViewModel
        public KategorieEditView(KategorieEditViewModel vm)
        {
            InitializeComponent();

            // Nastavíme DataContext, aby grafika viděla data z ViewModelu
            DataContext = vm;
        }
    }
}