using System.Windows;
using PujcovnaAut.ViewModels;

namespace PujcovnaAut
{
    public partial class ZakaznikEditView : Window
    {
        public ZakaznikEditView(ZakaznikEditViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
        public ZakaznikEditView() { InitializeComponent(); }
    }
}