namespace bib_ian_mondelaers;

internal class NewsPaper: ReadingRoomItem
{
    // Eigenschappen
    private DateTime date;
    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }
    private string identification;
    public override string Identification
    {
        get 
        {
            string[] newsPaperWithoutSpaces = Title.Split(" ");
            string initials = "";
            foreach (string item in newsPaperWithoutSpaces)
            {
                initials += item[0];
            }
            string datePart = Date.ToString("ddMMyyyy");
            identification = initials.ToUpper() + datePart;
            return identification; 
        }
    }
    private string categorie;
    public override string Categorie
    {
        get 
        {
            categorie = Category.Krant.ToString();
            return categorie; 
        }
    }
    
    // Constructors
    public NewsPaper(string title, string publisher, DateTime date): base(title, publisher)
    {
        this.date = date;
    }

    // Methodes

    /// <summary>
    /// Methode die verantwoordelijk is voor het deserialiseren van kranten uit een csv-bestand
    /// </summary>
    /// <param name="csvFilePath"></param>
    /// <returns></returns>
    public static List<NewsPaper> DeserializeNewspapersFromCsv(string csvFilePath)
    {
        List<NewsPaper> newspapers = new List<NewsPaper>();
        Console.WriteLine("Kranten lezen uit CSV-bestand...");
        string[] lines = File.ReadAllLines(csvFilePath);
        Console.WriteLine("CSV-gegevens verwerken...");
        foreach (string line in lines.Skip(1))
        {
            string[] data = line.Split(';');
            if (data.Length != 3)
            {
                Console.WriteLine("Onvolledige gegevens in CSV-bestand. Regel wordt overgeslagen.");
                continue;
            }
            string title = data[0];
            string publisher = data[1];
            DateTime date = DateTime.Parse(data[2]);
            NewsPaper newspaper = new NewsPaper(title, publisher, date);
            newspapers.Add(newspaper);
        }
        Console.WriteLine($"Succesvol {newspapers.Count} kranten gedeserialiseerd uit CSV-bestand.\n");
        return newspapers;
    }
}
