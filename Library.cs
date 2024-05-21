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

        /// <summary>
        /// Constructor met 1 parameter
        /// </summary>
        /// <param name="name"></param>
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
            if(!Books.Contains(book))
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
    }
}