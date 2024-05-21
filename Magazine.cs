namespace bib_ian_mondelaers;

internal class Magazine: ReadingRoomItem
{
    // Eigenschappen
    private byte month;
    public byte Month
    {
        get { return month; }
        set 
        {
            if(value >= 1 && value <= 12)
            {
                month = value; 
            }
            else
            {
                throw new ArgumentOutOfRangeException("De maand is maximaal 12 en groter dan 1");
            }
        }
    }
    private uint year;
    public uint Year
    {
        get { return year; }
        set 
        {
            if(value <=2500)
            {
                year = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Het jaartal is maximaal 2500");
            }
        }
    }
    private string identification;
    public override string Identification
    {
        get 
        {
            string[] magazineWithouSpaces = Title.Split(" ");
            string initials = "";
            foreach (string item in magazineWithouSpaces)
            {
                initials += item[0];
            }
            string monthPart = Month.ToString("00");
            string yearPart = Year.ToString("0000");
            identification = initials.ToUpper() + monthPart + yearPart;
            return identification;
        }
    }
    private string categorie;
    public override string Categorie
    {
        get 
        { 
            categorie = Category.Maandblad.ToString();
            return categorie; 
        }
    }
    
    // Constructors

    /// <summary>
    /// Constructor met 4 parameters
    /// </summary>
    /// <param name="title"></param>
    /// <param name="publisher"></param>
    /// <param name="month"></param>
    /// <param name="year"></param>
    public Magazine(string title, string publisher, byte month, uint year): base(title, publisher)
    {
        this.month = month;
        this.year = year;
    }

    // Methodes

    /// <summary>
    /// Methode die verantwoordelijk is voor het deserialiseren van maandbladen uit een csv-bestand
    /// </summary>
    /// <param name="csvFilePath"></param>
    /// <returns></returns>
    public static List<Magazine> DeserializeMagazinesFromCsv(string csvFilePath)
    {
        List<Magazine> magazines = new List<Magazine>();
        Console.WriteLine("Maandbladen lezen uit CSV-bestand...");
            string[] lines = File.ReadAllLines(csvFilePath);
            Console.WriteLine("CSV-gegevens verwerken...");
            foreach(string line in lines.Skip(1))
            {
                string[] data = line.Split(';');
                if(data.Length != 4)
                {
                    Console.WriteLine("Onvolledige gegevens in CSV-bestand. Regel wordt overgeslagen.");
                    continue;
                }
                string title = data[0];
                string publisher = data[1];
                byte month = byte.Parse(data[2]);
                uint year = uint.Parse(data[3]);
                Magazine magazine = new Magazine(title, publisher, month, year);
                magazines.Add(magazine);
            }
            Console.WriteLine($"Succesvol {magazines.Count} maandbladen gedeserialiseerd uit CSV-bestand.\n");
        return magazines;
    }
}
