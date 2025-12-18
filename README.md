# PujcovnaAut
🚗 Správa autopůjčovny (WPF + Entity Framework)
Tento projekt představuje desktopovou aplikaci pro správu autopůjčovny vytvořenou v rámci semestrální práce. Aplikace umožňuje komplexní evidenci vozidel, zákazníků a správu výpůjček s automatickým výpočtem cen za vypůjčení a pojištění a kontrolou dostupnosti.

🛠️ Klíčové vlastnosti
  Dispečink výpůjček: Přehledná správa aktivních i ukončených pronájmů.
  Chytrá logika dostupnosti: Při vytváření nové výpůjčky systém automaticky detekuje kolize termínů a zašedne nedostupná vozidla.
  Automatická kalkulace: Výpočet konečné ceny na základě kategorie vozidla (koeficienty), zvoleného typu pojištění a délky pronájmu.
  Evidence entit: Správa vozového parku (SPZ, kategorie, stavy) a databáze zákazníků.
  Architektura: Projekt využívá vzor MVVM pro čisté oddělení logiky od uživatelského rozhraní a Entity Framework Core pro komunikaci s SQL databází.

🚀 Instrukce k instalaci a spuštění
Pro správné fungování aplikace na jiném počítači postupujte podle následujících kroků:
  Klonování projektu: Stáhněte projekt pomocí Git Clone nebo jako ZIP archiv.
  Obnova balíčků: Po otevření v MS Visual Studio klikněte pravým tlačítkem na Solution a zvolte Restore NuGet Packages.
  Vytvoření databáze: Aplikace využívá LocalDB. Pro vytvoření databázových tabulek a naplnění testovacími daty (10 aut, 10 zákazníků) otevřete Package Manager Console a zadejte příkaz:
    Update-Database
  Spuštění: Nyní lze aplikaci spustit pomocí tlačítka Start (F5).

📊 Použité technologie
  Jazyk: C# (.NET Core)
  UI: WPF (Windows Presentation Foundation)
  Databáze: MS SQL Server (LocalDB)
  ORM: Entity Framework Core
