using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PujcovnaAut.ViewModels
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        // Událost, která informuje grafické rozhraní o změně dat.
        public event PropertyChangedEventHandler PropertyChanged;

        // Metoda pro vyvolání události při změně vlastnosti.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}