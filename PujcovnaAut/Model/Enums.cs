namespace PujcovnaAut.Model
{
    // Stavy pro AUTO
    public enum StavAuta
    {
        Volne = 0,
        Pujcene = 1,  // Opraveno z "Vypujcene" na "Pujcene"
        Servis = 2,
        Vyrazene = 3  // Přidáno, protože to kód vyžadoval
    }

    // Stavy pro VÝPŮJČKU
    public enum StavVypujcky
    {
        Rezervovano = 0,
        Zapujceno = 1,
        Ukonceno = 2
    }
}