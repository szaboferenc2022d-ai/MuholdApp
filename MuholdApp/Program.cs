using MuholdApp;

int hibasSorokSzama =0;

List<Muhold> muholdak = new List<Muhold>();

foreach(string sor in File.ReadAllLines("muholdak.txt").Skip(1))
{
    if (string.IsNullOrWhiteSpace(sor))
    {
        hibasSorokSzama++;
        continue;
    };
    
    string[] adatok = sor.Split('|');
    if (adatok.Count() != 4) 
    {
        hibasSorokSzama++;
        continue;
    }
    
    string tipus = adatok[0];
    if(tipus != "M" && tipus != "K")
    {
        hibasSorokSzama++;
        continue;
    }

    if(string.IsNullOrWhiteSpace(adatok[1]) || !int.TryParse(adatok[2], out int sulyKg) || !int.TryParse(adatok[3], out int extra))
    {
        hibasSorokSzama++;
        continue;
    }

    if (tipus == "M")
    {
        muholdak.Add(new Muhold(adatok[1], sulyKg, extra));
    }
    else
    {
        muholdak.Add(new KutatasiMuhold(adatok[1], sulyKg, extra, extra));
    }
}

Console.WriteLine($"Sikeresen beolvasott műholdak: {muholdak.Count} db");
Console.WriteLine($"Hibás sorok száma: {hibasSorokSzama} db\n");

Console.WriteLine("Beolvasott műholdak listája:");
foreach (var muhold in muholdak)
{
    Console.WriteLine(muhold.ToString());
}

Console.WriteLine("LINQ Statisztikák:");

int atlagTomeg = (int)muholdak.Average(m => m.SulyKg);
Console.WriteLine($"Műholdak átlagos tömege: {atlagTomeg} kg");

int legNehezeb = muholdak.Max(m => m.SulyKg);
Console.WriteLine($"Legnehezebb műhold: {muholdak.First(m => m.SulyKg == legNehezeb).Azonosito} ({legNehezeb} kg)");

int napiKoltseg = muholdak.Max(m => m.NapiKoltseg());
Console.WriteLine($"Legalacsonyabb napi költségű műhold: {napiKoltseg.Azonosito} ({napiKoltseg} EUR)");

int minSulykg = muholdak.Count(m => m.SulyKg >= 1000);
Console.WriteLine($"Legalább 1000 kg-os műholdak száma: {minSulykg} db\n");

Console.WriteLine("Kutatási műholdak külön elemzése:");
Console.WriteLine($"- {muholdak.First(m => m.Azonosito == "EXP-202").Azonosito} kutatási műhold műszereinek száma: {muholdak.First(m => m.Azonosito == "EXP-202").extra} db.");
Console.WriteLine($"- {muholdak.First(m => m.Azonosito == "EXP-307").Azonosito} kutatási műhold műszereinek száma: {muholdak.First(m => m.Azonosito == "EXP-307").extra} db.");

var kiir = muholdak.Where(m => m is KutatasiMuhold).Select(m => (KutatasiMuhold)m).OrderByDescending(m => m.MuszerekSzama);
Console.WriteLine($"A riport.txt, {kiir}");