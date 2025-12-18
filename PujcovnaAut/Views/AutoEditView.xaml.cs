using System.Windows;
using PujcovnaAut.ViewModels; // Nutné pro AutoEditViewModel

namespace PujcovnaAut.Views
{
    /// <summary>
    /// Interaction logic for AutoEditView.xaml
    /// </summary>
    public partial class AutoEditView : Window
    {
        // TENTO konstruktor ti chybí – přidej ho tam
        public AutoEditView(AutoEditViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        // Původní prázdný konstruktor tam nech také
        public AutoEditView()
        {
            InitializeComponent();
        }
    }
}