using System.ComponentModel;

namespace DataEntity
{
    /// <summary>
    /// Definuje možné stavy, ve kterých se může nacházet vozidlo v evidenci.
    /// </summary>
    public enum StavAuta
    {
        // Je indikováno, že vozidlo je připraveno k okamžitému pronájmu.
        [Description("Volné")]
        Volne = 0,

        // Je indikováno, že vozidlo je aktuálně předmětem aktivní výpůjčky.
        [Description("Půjčené")]
        Pujcene = 1
    }

    /// <summary>
    /// Definuje životní cyklus záznamu o výpůjčce od jejího založení až po uzavření.
    /// </summary>
    public enum StavVypujcky
    {
        // Je indikována aktivní fáze výpůjčky, kdy je vozidlo u zákazníka.
        [Description("Zapůjčeno")]
        Zapujceno = 0,

        // Je indikováno řádné ukončení výpůjčky a vrácení vozidla do autopůjčovny.
        [Description("Ukončeno")]
        Ukonceno = 1,

        // Je indikováno stornování výpůjčky (např. před jejím faktickým zahájením).
        [Description("Zrušeno")]
        Zruseno = 2
    }
}