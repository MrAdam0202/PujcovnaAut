using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace PujcovnaAut
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // --- 1. ČÁST: NASTAVENÍ ČEŠTINY A MĚNY (Kč) ---
            var czechCulture = new CultureInfo("cs-CZ");

            // Nastavíme kulturu pro vlákna (formát čísel, dat, měny)
            Thread.CurrentThread.CurrentCulture = czechCulture;
            Thread.CurrentThread.CurrentUICulture = czechCulture;
            CultureInfo.DefaultThreadCurrentCulture = czechCulture;
            CultureInfo.DefaultThreadCurrentUICulture = czechCulture;

            // Donutíme WPF XAML, aby používal češtinu (pro StringFormat Bindingy)
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(czechCulture.IetfLanguageTag)));

            // --- 2. ČÁST: TVOJE PŮVODNÍ LOGIKA ---

            // Inicializace globálního kontextu a připojení k databázi.
            Globals.Initialize();

            // Vytvoření a zobrazení hlavního okna aplikace.
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}