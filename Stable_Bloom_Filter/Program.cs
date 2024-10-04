using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // stable Bloom Filter anlegen mit 1500 slots und 4 Hash-Funktions aufrufe mit jeweils anderem seed
        // Hierbei ist im default parameter schon festgelegt dass jeder slot 5 mal beschrieben werden kann.
        // Der Verfall Faktor liegt bei 25% basis wert, wird allerdings von der Klasse im Verlauf nach-justiert je nach Belegung.
        StableBloom filter = new StableBloom(1500, 4);

        List<string> elemente = new List<string>
        {
            "Löwe",
            "Zebra",
            "Maus",
            "Affe",
            "Tiger",
            "Katze"
        };

        elemente.ForEach(itm => filter.Add(itm));
        elemente.ForEach(itm => Console.WriteLine($"Filterabfrage für Element '{itm}' ergibt : {filter.Contains(itm)}."));


        // Verfall der slots testen indem wir die Grenze von 100 eintragungen bis zum Verfall überschreiten 
        for (int i = 0; i < 250; i++)
            filter.Add($"item{i}");


        // Nochmal alle vorherigen Werte abfragen
        elemente.ForEach(itm => Console.WriteLine($"Filterabfrage für Element '{itm}' ergibt : {filter.Contains(itm)}."));

    }
}
