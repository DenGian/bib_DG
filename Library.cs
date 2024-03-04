namespace bib_ian_mondelaers
{
    public class Library
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

        // Constructors
        public Library(string name)
        {
            Name = name;
            Books = new List<Book>();
        }

        // Methodes
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
        public void RemoveBook(Book book)
        {
            if (Books.Contains(book))
            {
                Books.Remove(book);
            }
            else
            {
                Console.WriteLine("Dit boek bestaat niet in de bibliotheek.");
            }
        }
        public Book BookByTitleAuthor(string title, string author)
        {
            foreach (Book book in Books)
            {
                if(book.Title == title && book.Author == author)
                {
                    return book;
                }
            }
            return null;
        }
        public Book BookByIsbn(string isbn)
        {
            foreach (Book book in Books)
            {
                if(book.ISBN == isbn)
                {
                    return book;
                }
            }
            return null;
        }
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