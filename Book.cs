namespace bib_ian_mondelaers
{
    internal class Book: ILendable
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
                    throw new InvalidTitleException();
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
                    throw new InvalidISBNException();
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
                    throw new InvalidReleaseDateException();
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
                    throw new InvalidPageCountException();
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
                    throw new InvalidPriceException();
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
        private bool isAvailable;
        public bool IsAvailable
        {
            get { return this.isAvailable; }
            set { this.isAvailable = value; }
        }
        private DateTime borrowingDate;
        public DateTime BorrowingDate
        {
            get { return this.borrowingDate; }
            set { this.borrowingDate = value; }
        }
        private int borrowDays;
        public int BorrowDays
        {
            get { return this.borrowDays; }
            set 
            { 
                this.borrowDays = value;
            }
        }
    
        // methodes

        /// <summary>
        /// Methode die nagaat of een ingegeven ISBN de standaard ISBN regels volgt
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Methode die alle info van een boek weergeeft
        /// </summary>
        public void AllBookInfo()
        {
            Console.WriteLine($"Titel: {Title}");
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine($"Auteur: {Author}");
            Console.WriteLine($"Datum van uitgave: {ReleaseDate:dd-MM-yyyy}");
            Console.WriteLine($"Aantal pagina's: {NumberOfPages}");
            Console.WriteLine($"Genre: {BookGenre}");
            Console.WriteLine($"Uitgever: {Publisher}");
            Console.WriteLine($"Prijs: {Price}");
        }
        /// <summary>
        /// Methode die verantwoordelijk is voor het deserialiseren van boeken uit een csv file
        /// </summary>
        /// <param name="csvFilePath"></param>
        /// <returns></returns>
        public static List<Book> DeserializeBooksFromCsv(string csvFilePath)
        {
            List<Book> books = new List<Book>();
            Console.WriteLine("Boeken lezen uit CSV-bestand...");
            string[] lines = File.ReadAllLines(csvFilePath);
            Console.WriteLine("CSV-gegevens verwerken...");
            foreach(string line in lines.Skip(1))
            {
                string[] data = line.Split(';');
                if(data.Length != 8)
                {
                    Console.WriteLine("Onvolledige gegevens in CSV-bestand. Regel wordt overgeslagen.");
                    continue;
                }
            string title = data[0];
            string isbn = data[1];
            string author = data[2];
            DateTime releaseDate = DateTime.Parse(data[3]);
            int numberOfPages = int.Parse(data[4]);
            string genreString = data[5];
            Genre genre;
            switch(genreString.ToLower())
            {
                case "fiction":
                    genre = Genre.Fiction;
                    break;
                case "nonfiction":
                    genre = Genre.NonFiction;
                    break;
                case "romance":
                    genre = Genre.Romance;
                    break;
                case "thriller":
                    genre = Genre.Thriller;
                    break;
                case "sciencefiction":
                    genre = Genre.ScienceFiction;
                    break;
                case "childrensliterature":
                    genre = Genre.ChildrensLiterature;
                    break;
                case "schoolboek":
                    genre = Genre.Schoolboek;
                    break;
                default:
                    Console.WriteLine($"Ongeldig genre '{genreString}'. Boekcreatie wordt overgeslagen.");
                    continue;
            }
            string publisher = data[6];
            decimal price = decimal.Parse(data[7]);
            Book book = new Book(title, isbn, author, releaseDate, numberOfPages, genre, publisher, price);
            books.Add(book);
            }
            Console.WriteLine($"Succesvol {books.Count} boeken gedeserialiseerd uit CSV-bestand.");
            Console.WriteLine();
            return books;
        }
        /// <summary>
        /// Methode om de ISBN aan een boek toe te voegen
        /// </summary>
        /// <param name="book"></param>
        public static void AddISBN(Book book)
        {
            Console.Write("\nVoer de ISBN van het boek in: ");
            string isbn = Console.ReadLine();
            try
            {
                if(Book.ValidISBN(isbn))
                {
                    book.ISBN = isbn;
                    Console.WriteLine("\nInformatie succesvol toegevoegd aan het boek.");
                }
                else
                {
                    throw new InvalidISBNException();
                }
            }
            catch(InvalidISBNException iiex)
            {
                Console.WriteLine("\nFout bij het toevoegen van ISBN: " + iiex.ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine("\nEr is een fout opgetreden bij het toevoegen van ISBN: " + e.Message);
            }
        }
        /// <summary>
        /// Methode om een genre aan een boek toe te voegen
        /// </summary>
        /// <param name="book"></param>
        public static void AddGenre(Book book)
        {
            Console.WriteLine("\nKies het genre van het boek:");
            for(int i = 0; i < 7; i++)
            {
                Genre genre = (Genre)i;
                Console.WriteLine($"{i + 1}. {genre}");
            }
            Console.Write("Selecteer een genre [1-7]: ");
            int genreChoice;
            try
            {
                genreChoice = int.Parse(Console.ReadLine());
                if(genreChoice < 1 || genreChoice > 7)
                {
                    throw new InvalidGenreException();
                }
                Genre selectedGenre = (Genre)(genreChoice - 1);
                book.BookGenre = selectedGenre;
                Console.WriteLine("\nInformatie succesvol toegevoegd aan het boek.");
            }
            catch(FormatException)
            {
                Console.WriteLine("\nOngeldige invoer voor genre. Voer a.u.b. een getal in.");
            }
            catch(InvalidGenreException igex)
            {
                Console.WriteLine("\nFout bij het toevoegen van het genre: " + igex.ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine("\nEr is een fout opgetreden bij het toevoegen van het genre: " + e.Message);
            }
        }
        /// <summary>
        /// Methode om een uitgeverij aan een boek toe te voegen
        /// </summary>
        /// <param name="book"></param>
        public static void AddPublisher(Book book)
        {
            try
            {
                Console.Write("\nVoer de uitgeverij van het boek in: ");
                string publisher = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(publisher))
                {
                    book.Publisher = publisher;
                    Console.WriteLine("\nInformatie succesvol toegevoegd aan het boek.");
                }
                else
                {
                    throw new InvalidPublisherException();
                }
            }
            catch(InvalidPublisherException ipex)
            {
                Console.WriteLine("\nFout bij het toevoegen van de uitgeverij: " + ipex.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("\nEr is een fout opgetreden bij het toevoegen van de uitgeverij: " + e.Message);
            }
        }
        /// <summary>
        /// Methode om een boek te kunnen ontlenen
        /// </summary>
        public void Borrow()
        {
            if(IsAvailable)
            {
                if(BorrowingDate == default(DateTime))
                {
                    Console.WriteLine("Voer de uitleendatum in (dd/mm/yyyy): ");
                    BorrowingDate = DateTime.Parse(Console.ReadLine());
                }
                IsAvailable = false;
                if(BookGenre == Genre.Schoolboek)
                {
                    BorrowDays = 10;
                }
                else
                {
                    BorrowDays = 20;
                }
                Console.WriteLine($"\nUitleendatum: {BorrowingDate.ToShortDateString()}. \nDit boek moet voor {BorrowingDate.AddDays(BorrowDays).ToShortDateString()} teruggebracht worden.");
            }
            else
            {
                Console.WriteLine("\nDit boek is momenteel niet beschikbaar om te lenen.");
            }
        }
        /// <summary>
        /// Methode om een boek terug te brengen
        /// </summary>
        public void Return()
        {
            if(!IsAvailable)
            {
                IsAvailable = true;
                DateTime returnDate = DateTime.Now;
                if (returnDate > BorrowingDate.AddDays(BorrowDays))
                {
                    Console.WriteLine("\nHet boek is te laat teruggebracht.");
                }
                else
                {
                    Console.WriteLine("\nHet boek is op tijd teruggebracht.");
                }
            }
            else
            {
                Console.WriteLine("\nDit boek is niet uitgeleend.");
            }
        }

        //constructors

        /// <summary>
        /// Constructor met 3 paramters
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="library"></param>
        public Book(string title, string author, Library library)
        {
            this.IsAvailable = true;
            this.BorrowDays = 20;
            this.Title = title;
            this.Author = author;
            this.Library = library;
        }
        /// <summary>
        /// Constructor met 8 parameters
        /// </summary>
        /// <param name="title"></param>
        /// <param name="isbn"></param>
        /// <param name="author"></param>
        /// <param name="releaseDate"></param>
        /// <param name="numberOfPages"></param>
        /// <param name="genre"></param>
        /// <param name="publisher"></param>
        /// <param name="price"></param>
        public Book(string title, string isbn, string author, DateTime releaseDate, int numberOfPages, Genre genre, string publisher, decimal price)
        {
            this.IsAvailable = true;
            if (genre == Genre.Schoolboek)
            {
                this.BorrowDays = 10;
            }
            else
            {
                this.BorrowDays = 20;
            }
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