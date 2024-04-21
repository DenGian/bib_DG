namespace bib_ian_mondelaers
{
    internal class Library
    {
        // Eigenschappen
        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private List<Book> books;
        public List<Book> Books
        {
            get { return books; }
            private set { books = value; }
        }
        private Dictionary<DateTime, ReadingRoomItem> allReadingRoom = new Dictionary<DateTime, ReadingRoomItem>();
        public Dictionary<DateTime, ReadingRoomItem> AllReadingRoom
        {
            get { return allReadingRoom; }
        }
        

        // Constructors
        public Library(string name)
        {
            Name = name;
            Books = new List<Book>();
        }

        // Methodes

        /// <summary>
        /// Methode om een boek toe te voegen
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            if (!Books.Contains(book))
            {
                Books.Add(book);
            }
            else
            {
                Console.WriteLine("Dit boek bestaat al in de bibliotheek.");
            }
        }

        /// <summary>
        /// Methode om een boek te verwijderen
        /// </summary>
        /// <param name="book"></param>
        public void RemoveBook(Book book)
        {
            if(Books.Contains(book))
            {
                Books.Remove(book);
            }
            else
            {
                Console.WriteLine("Dit boek bestaat niet in de bibliotheek.");
            }
        }

        /// <summary>
        /// Methode om een boek te vinden aan de hand van de titel en de auteur
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public Book BookByTitleAuthor(string title, string author)
        {
            foreach(Book book in Books)
            {
                if(book.Title == title && book.Author == author)
                {
                    return book;
                }
            }
            return null;
        }

        /// <summary>
        /// Methode om een boek te vinden aan de hand van de ISBN nummer
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public Book BookByIsbn(string isbn)
        {
            foreach(Book book in Books)
            {
                if(book.ISBN == isbn)
                {
                    return book;
                }
            }
            return null;
        }

        /// <summary>
        /// Methode die alle boeken van een gekozen auteur teruggeeft
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public List<Book> AllBooksByAuthor(string author)
        {
            List<Book> authorBooks = new List<Book>();
            foreach(Book book in Books)
            {
                if(book.Author == author)
                {
                    authorBooks.Add(book);
                }
            }
            return authorBooks;
        }

        /// <summary>
        /// Methode die alle boeken door een gekozen uitgeverij teruggeeft
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        public List<Book> AllBooksByPublisher(string publisher)
        {
            List<Book> publisherBooks = new List<Book>();
            foreach(Book book in Books)
            {
                if(book.Publisher == publisher)
                {
                    publisherBooks.Add(book);
                }
            }
            return publisherBooks;
        }

        /// <summary>
        /// Methode om de ISBN aan een boek toe te voegen
        /// </summary>
        /// <param name="book"></param>
        public static void AddISBN(Book book)
        {
            Console.Write("Voer de ISBN van het boek in: ");
            string isbn = Console.ReadLine();
            if (Book.ValidISBN(isbn))
            {
                book.ISBN = isbn;
                Console.WriteLine("\nInformatie succesvol toegevoegd aan het boek.");
            }
            else
            {
                Console.WriteLine("Ongeldige ISBN.");
            }
        }

        /// <summary>
        /// Methode om een genre aan een boek toe te voegen
        /// </summary>
        /// <param name="book"></param>
        public static void AddGenre(Book book)
        {
            Console.WriteLine("\nKies het genre van het boek:");
            for (int i = 0; i < 6; i++)
            {
                Genre genre = (Genre)i;
                Console.WriteLine($"{i + 1}. {genre}");
            }
            Console.Write("Selecteer een genre [1-6]: ");
            int genreChoice = int.Parse(Console.ReadLine());
            if (genreChoice >= 1 && genreChoice <= 6)
            {
                Genre selectedGenre = (Genre)(genreChoice - 1);
                book.BookGenre = selectedGenre;
                Console.WriteLine("\nInformatie succesvol toegevoegd aan het boek.");
            }
            else
            {
                Console.WriteLine("Ongeldige invoer voor genre.");
            }
        }

        /// <summary>
        /// Methode om een uitgeverij aan een boek toe te voegen
        /// </summary>
        /// <param name="book"></param>
        public static void AddPublisher(Book book)
        {
            Console.Write("Voer de uitgeverij van het boek in: ");
            string publisher = Console.ReadLine();
            book.Publisher = publisher;
            Console.WriteLine("\nInformatie succesvol toegevoegd aan het boek.");
        }
    }
}