using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PujcovnaAut.ViewModels
{
    /// <summary>
    /// Základní třída pro všechny ViewModely v aplikaci.
    /// Je implementováno rozhraní INotifyPropertyChanged, které zajišťuje propojení mezi logikou a grafickým rozhraním.
    /// </summary>
    public abstract class BaseVM : INotifyPropertyChanged
    {
        // Je definována událost, která je vyvolána při změně hodnoty jakékoli vlastnosti ve ViewModelu.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metoda slouží k informování uživatelského rozhraní o změně konkrétní vlastnosti.
        /// Je využíván atribut CallerMemberName, díky kterému je název vlastnosti doplněn automaticky kompilátorem.
        /// </summary>
        /// <param name="propertyName">Název změněné vlastnosti.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Událost je vyvolána pouze v případě, že je k ní přihlášen alespoň jeden odběratel (typicky vazba v XAML).
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}