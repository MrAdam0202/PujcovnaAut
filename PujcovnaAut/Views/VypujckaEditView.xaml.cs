using System.Windows;
using System.Windows.Controls;
using PujcovnaAut.ViewModels;

namespace PujcovnaAut.Views
{
    public partial class VypujckaEditView : Window
    {
        public VypujckaEditView(VypujckaEditViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        // Tlačítko OK
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        // Když se zavře kalendář (změna data) -> přepočítat
        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (DataContext is VypujckaEditViewModel vm)
            {
                vm.PriZmeneData();
            }
        }

        // Když se změní pojištění -> přepočítat
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is VypujckaEditViewModel vm)
            {
                vm.PriZmeneData(); // Zneužijeme stejnou metodu pro refresh
            }
        }
    }
}