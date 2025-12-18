using System.Windows;
using PujcovnaAut.ViewModels;

namespace PujcovnaAut
{
    /// <summary>
    /// Interaction logic for PojisteniEditView.xaml
    /// </summary>
    public partial class PojisteniEditView : Window
    {
        // 1. Hlavní konstruktor, který voláš z MainViewModelu
        // Přijme ViewModel a nastaví ho jako DataContext
        public PojisteniEditView(PojisteniEditViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        // 2. Prázdný konstruktor (pro jistotu, aby fungoval náhled v editoru)
        public PojisteniEditView()
        {
            InitializeComponent();
        }
    }
}