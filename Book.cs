namespace bib_ian_mondelaers
{
    internal class Book
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
            
            switch (genreString.ToLower())
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