using System.ComponentModel;

namespace DataEntity
{
    public enum StavAuta
    {
        [Description("Volné")]
        Volne = 0,     // Přejmenováno z "Dostupne" (váš kód chce Volne)

        [Description("Půjčené")]
        Pujcene = 1
    }

    public enum StavVypujcky
    {
        [Description("Zapůjčeno")]
        Zapujceno = 0, // Přejmenováno z "Aktivni" (váš kód chce Zapujceno)

        [Description("Ukončeno")]
        Ukonceno = 1,  // Přejmenováno z "Vraceno" (váš kód chce Ukonceno)

        [Description("Zrušeno")]
        Zruseno = 2
    }
}