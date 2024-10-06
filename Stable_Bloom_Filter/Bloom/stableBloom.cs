using Stable_Bloom_Filter.Hash;
using System.Collections;
public class StableBloom
{
    private Random random;

    /// <summary>
    /// Die slots für den Bloom Filter
    /// </summary>
    private BitArray[] table;

    /// <summary>
    /// Anzahl der zu verwendenden Hashes
    /// </summary>
    private int numHashFunctions;

    /// <summary>
    /// Maximale Kapazität pro slot
    /// </summary>
    private int maxCounterValue;

    /// <summary>
    /// Der Verfall Faktor für jeden slot
    /// </summary>
    private double decayFactor;


    /// <summary>
    /// Bloom Filter Faktor für maximale Nutzung des Filters
    /// </summary>
    private double maxTakenSpaceFactor = 0.9;

    /// <summary>
    /// Maximale Eintragungen bevor Verfall in erwägung gezogen wird
    /// </summary>
    private int maxInsertions = 100;

    /// <summary>
    /// temporärer stand der Eintragungen
    /// </summary>
    private int curInsertions = 0;


    /// <summary>
    /// Einen neuen Bloom Filter erstellen
    /// </summary>
    /// <param name="size">Größe des Filters</param>
    /// <param name="numHashFunctions">Anzahl der Hashwerte die verwendet werden sollen</param>
    /// <param name="maxCounterValue">Maximale Kapazität eines slots im Filter</param>
    /// <param name="decayFactor">Der Verfall Faktor für die einzelnen slots</param>
    public StableBloom(int size, int numHashFunctions, int maxCounterValue = 5, double decayFactor = 0.25)
    {
        if (size < 10) size = 10;

        table = new BitArray[size];
        for (int i = 0; i < size; i++)
            table[i] = new BitArray(maxCounterValue);

        this.numHashFunctions = numHashFunctions;
        random = new Random();
        this.maxCounterValue = maxCounterValue;
        this.decayFactor = decayFactor;
    }

    // Hashfunktion mit einem Seed (für mehrere Hashfunktionen verwendet)

    private int CreateHash(string item, int seed)
        => Math.Abs((int)MurmurHash3.MurmurHash3_32(item, (uint)seed) % (table.Length - 1));


    // Füge ein Element in den Stable Bloom Filter ein
    public void Add(string item)
    {
        // Die selektierten Stellen erhöhen
        for (int i = 0; i < numHashFunctions; i++)
        {
            int index = CreateHash(item, i);
            table[index].Set(Math.Min(maxCounterValue, table[index].Cast<bool>().Count(x => x)), true);  // Zähler erhöhen
        }
        curInsertions++; // Eintragungs Zyklus zählen

        if (curInsertions == maxInsertions) // beim erreichen der maximalen Eintragungen pro Zyklus, Verfall auslösen
        {
            Decay(); // Verfall-Funktion auslösen
            AdjustDecayFactor(); // dynamisches justieren des Verfall-Faktors
            curInsertions = 0;
        }


    }

    // Prüfen, ob ein Element im Filter enthalten ist
    public bool Contains(string item)
    {
        // Einzelne Hashes durchgehen, als seed wird einfach der derzeitige Index verwendet
        // so ist gewährleistet das verschiedene Werte zustande kommen
        for (int i = 0; i < numHashFunctions; i++)
        {
            int index = CreateHash(item, i);
            if (!table[index].HasAnySet()) // sobald einer der selektierten slots 0 ist, ist das Element nicht vorhanden
                return false;
        }
        return true; // Alle Hash-Positionen haben nicht-null Zähler, Element könnte also vorhanden sein
    }

    /// <summary>
    /// Werte auf basis des Verfall-Faktors neu besetzen.
    /// </summary>
    private void Decay()
    {
        int kslots = 0;
        // Bloom Filter durchgehen
        for (int i = 0; i < table.Length; i++)
        {

            // Abfragen ob der derzeitige slot größer null ist und einen zufallsfaktor generieren.
            // ist der kleiner als der Verfall-Faktor wird der slot um 1 reduziert
            if (table[i].HasAnySet() &&
                random.NextDouble() < decayFactor)
            {
                table[i].RightShift(1);
                kslots++;
            }
        }
#if DEBUG
        Console.WriteLine($"-> Bereinigung ausgelöst, es wurden {kslots} von {table.Length} slots freigegeben.");
#endif
    }

    /// <summary>
    /// Justiert den verfall Faktor neu je nach Belegung des Filters
    /// </summary>
    private void AdjustDecayFactor()
    {
        double takenSpaceRatio = getRatio();

        if (takenSpaceRatio > maxTakenSpaceFactor)
            decayFactor += 0.01;
        else if (takenSpaceRatio < maxTakenSpaceFactor - 0.1 && decayFactor > 0.01)
            decayFactor -= 0.01;
    }

    /// <summary>
    /// Verhältnis der Belegung des Filters
    /// </summary>
    /// <returns></returns>
    private double getRatio()
    {
        int nonZeroCounters = 0;
        foreach (var count in table)
        {
            if (count.HasAnySet())
                nonZeroCounters++;
        }
        return (double)(nonZeroCounters / table.Length);
    }

}