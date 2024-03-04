namespace bib_ian_mondelaers
{
    public class Book
    {
    // Eigenschappen
    private string title;
    public string Title
    {
        get { return title; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Titel mag niet leeg zijn.");
            }
            title = value;
        }
    }
    private string isbn;
    public string ISBN
    {
        get { return isbn; }
        set
        {
            if(!ValidISBN(value))
            {
                throw new ArgumentException("Ongeldig ISBN");
            }
            isbn = value;
        }
    }
    private string author;
    public string Author
    {
        get { return author; }
        set
        {
            author = value;
        }
    }
    private DateTime releaseDate;
    public DateTime ReleaseDate
    {
        get { return releaseDate; }
        set 
        {
            if(value > DateTime.Now)
            {
                throw new ArgumentException("ReleaseDatum kan niet in de toekomst liggen.");
            }
            releaseDate = value;
        }
    }
    private int numberOfPages;
    public int NumberOfPages
    {
        get { return numberOfPages; }
        set 
        { 
            if(value <= 0)
            {
                throw new ArgumentException("Aantal pagina's moet een positief getal zijn.");
            }
            numberOfPages = value;
        }
    }
    
    private Genre genre;
    public Genre BookGenre
    {
        get { return genre; }
        set
        {
            genre = value;
        }
    }
    private string publisher;
    public string Publisher
    {
        get { return publisher; }
        set
        {
            publisher = value;
        }
    }

    private decimal price;
    public decimal Price
    {
        get { return price; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Prijs kan niet negatief zijn.");
            }
            price = value;
        }
    }
    private Library library;
    public Library Library
    {
        get { return library; }
        set
        {
            library = value;
        }
    }
    
    // methodes
    public static bool ValidISBN(string isbn)
    {
        string fullIsbn = isbn.Replace("-", "");
        if(fullIsbn.Length != 13)
        {
            return false;
        }
        foreach(char c in fullIsbn)
        {
            if(!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }

    public void AllBookInfo()
    {
        Console.WriteLine($"Titel: {Title}");
        Console.WriteLine($"ISBN: {ISBN}");
        Console.WriteLine($"Auteur: {Author}");
        Console.WriteLine($"Datum van uitgave: {ReleaseDate.ToString("dd-MM-yyyy")}");
        Console.WriteLine($"Aantal pagina's: {NumberOfPages}");
        Console.WriteLine($"Genre: {BookGenre}");
        Console.WriteLine($"Uitgever: {Publisher}");
        Console.WriteLine($"Prijs: {Price}");
    }

public static List<Book> DeserializeBooksFromCsv(string csvFilePath)
{
    List<Book> books = new List<Book>();

    Console.WriteLine("Reading books from CSV file...");
    string[] lines = File.ReadAllLines(csvFilePath);

    Console.WriteLine("Parsing CSV data...");
    foreach (string line in lines.Skip(1))
    {
        string[] data = line.Split(',');

        if (data.Length != 8)
        {
            Console.WriteLine("Incomplete data in CSV file. Skipping line.");
            continue;
        }

        string title = data[0];
        string isbn = data[1];
        string author = data[2];
        DateTime releaseDate = DateTime.Parse(data[3]);
        int numberOfPages = int.Parse(data[4]);
        Genre genre;
        if (!Enum.TryParse(data[5], true, out genre))
        {
            Console.WriteLine($"Invalid genre '{data[5]}'. Skipping book creation.");
            continue;
        }
        string publisher = data[6];
        decimal price = decimal.Parse(data[7]);

        Book book = new Book(title, isbn, author, releaseDate, numberOfPages, genre, publisher, price);

        books.Add(book);
    }

    Console.WriteLine($"Successfully deserialized {books.Count} books from CSV file.");
    Console.WriteLine("");
    return books;
}

    // Enum voor het genre van het boek
    public enum Genre
    {
        Fiction,
        NonFiction,
        Romance,
        Thriller,
        ScienceFiction
    }

    //constructors
    public Book(string title, string author, Library library)
    {
        this.Title = title;
        this.Author = author;
        this.Library = library;
    }
    public Book(string title, string isbn, string author, DateTime releaseDate, int numberOfPages, Genre genre, string publisher, decimal price)
    {
    this.Title = title;
    this.ISBN = isbn;
    this.Author = author;
    this.ReleaseDate = releaseDate;
    this.NumberOfPages = numberOfPages;
    this.BookGenre = genre;
    this.Publisher = publisher;
    this.Price = price;
    }       
    }   
}