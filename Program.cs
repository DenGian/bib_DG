using System;
using System.Collections.Generic;
using bib_ian_mondelaers;

namespace bib_ian_mondelaers
{
    class Program
    {
        static Library library;

        static void Main(string[] args)
        {
            // Creëer een nieuw library
            library = new Library("BI(an)B");

            Console.Clear();

            // Boeken van het csv bestand toevoegen aan de bib
            List<Book> mybooks = Book.DeserializeBooksFromCsv("./csv/books.csv");
            foreach(Book book in mybooks)
            {
                library.AddBook(book);
            }

            // Menu
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Keuzemenu:");
                Console.WriteLine("1. Voeg een boek toe");
                Console.WriteLine("2. Voeg informatie toe aan een boek (Uitgeverij)");
                Console.WriteLine("3. Toon alle info van een boek");
                Console.WriteLine("4. Zoek een boek");
                Console.WriteLine("5. Verwijder een boek");
                Console.WriteLine("6. Toon alle boeken in de bibliotheek");
                Console.WriteLine("7. Exit");
                Console.Write("Selecteer een optie [1-7]: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        AddBookInfo();
                        break;
                    case "3":
                        ShowBookInfo();
                        break;
                    case "4":
                        SearchBook();
                        break;
                    case "5":
                        DeleteBook();
                        break;
                    case "6":
                        ShowAllBooks();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze!");
                        break;
                }
            }
        }

        //Methoden

        /// <summary>
        /// Voeg een boek toe op basis van titel en auteur
        /// </summary>
        static void AddBook()
        {
            Console.Clear();

            Console.WriteLine("1. Voeg een boek toe");
            Console.WriteLine();

            Console.Write("Voer de titel van het boek in: ");
            string title = Console.ReadLine();
            Console.Write("Voer de auteur van het boek in: ");
            string author = Console.ReadLine();

            Book book = new Book(title, author, library);
            library.AddBook(book);

            Console.WriteLine();
            Console.WriteLine("Boek succesvol toegevoegd aan de bibliotheek.");

            Continue();
        }

        /// <summary>
        /// Voeg informatie aan een boek toe aan de hand van de uitgeverij
        /// </summary>
        static void AddBookInfo()
        {
            Console.Clear();
    
            Console.WriteLine("2. Voeg informatie toe aan een boek (Uitgeverij)");
            Console.WriteLine();

            Console.Write("Titel van het boek: ");
            string title = Console.ReadLine();
            Console.Write("Auteur van het boek: ");
            string author = Console.ReadLine();

            Book book = library.BookByTitleAuthor(title, author);

            Console.WriteLine();
            Console.Write("Uitgeverij van het boek: ");
            string publisher = Console.ReadLine();

            book.Publisher = publisher;

            Console.WriteLine();
            Console.WriteLine("Informatie succesvol toegevoegd aan het boek.");

            Continue();
        }

        /// <summary>
        /// Toon alle info van een boek a.d.h.v de titel en de auteur
        /// </summary>
        static void ShowBookInfo()
        {
            Console.Clear();
    
            Console.WriteLine("3. Toon alle info van een boek");
            Console.WriteLine();

            Console.Write("Titel van het boek: ");
            string title = Console.ReadLine();
            Console.Write("Auteur van het boek: ");
            string author = Console.ReadLine();

            Book book = library.BookByTitleAuthor(title, author);

            Console.WriteLine();
            Console.WriteLine("Informatie van het gevonden boek:");
            Console.WriteLine($"- Titel: {book.Title}");
            Console.WriteLine($"- ISBN: {book.ISBN}");
            Console.WriteLine($"- Auteur: {book.Author}");
            Console.WriteLine($"- Datum van uitgave: {book.ReleaseDate.ToString("dd-MM-yyyy")}");
            Console.WriteLine($"- Aantal pagina's: {book.NumberOfPages}");
            Console.WriteLine($"- Genre: {book.BookGenre}");
            Console.WriteLine($"- Uitgever: {book.Publisher}");
            Console.WriteLine($"- Prijs: {book.Price}");

            Continue();
        }

        /// <summary>
        /// Keuzemenu om een boek / boeken te zoeken op verschillende manieren
        /// </summary>
        static void SearchBook()
        {
        
            Console.Clear();
            
            Console.WriteLine("4. Zoek een boek");
            Console.WriteLine();
            Console.WriteLine("1. Zoek op ISBN");
            Console.WriteLine("2. Zoek op uitgever");
            Console.Write("Selecteer een zoekoptie [1-2]: ");
            
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SearchBookByISBN();
                    break;
                case "2":
                    SearchBookByPublisher();
                    break;
                default:
                    Console.WriteLine("Ongeldige keuze!");
                    break;
            }
        }
        
        /// <summary>
        /// Zoek een boek op basis van het ISBN
        /// </summary>
        static void SearchBookByISBN()
        {
            Console.WriteLine();
            Console.Write("Voer het ISBN van het boek in: ");
            string isbn = Console.ReadLine();

            if(!Book.ValidISBN(isbn))
            {
                Console.WriteLine();
                Console.WriteLine("Ongeldig ISBN.");
                Continue();
                return;
            }

            Book book = library.BookByIsbn(isbn);
            if(book == null)
            {
                Console.WriteLine();
                Console.WriteLine("Boek niet gevonden.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Gevonden boek:");
                Console.WriteLine($"Titel: {book.Title}, Auteur: {book.Author}");
            }

            Continue();
        }
        
        /// <summary>
        /// Zoek een boek op basis van de uitgeverij
        /// </summary>
        static void SearchBookByPublisher()
        {
            Console.WriteLine();
            Console.Write("Voer de uitgever van het boek in: ");
            string publisher = Console.ReadLine();
            
            List<Book> books = library.AllBooksByPublisher(publisher);
            if(books.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Geen boeken gevonden voor deze uitgever.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Gevonden boeken:");
                foreach (Book book in books)
                {
                    Console.WriteLine($"- Titel: {book.Title}, Auteur: {book.Author}");
                }
            }
            
            Continue();
        }

        /// <summary>
        /// Verwijder eem boek op basis van titel en auteur
        /// </summary>
        static void DeleteBook()
        {
            Console.Clear();

            Console.WriteLine("5. Verwijder een boek");
            Console.WriteLine();

            Console.Write("Titel van het boek: ");
            string title = Console.ReadLine();
            Console.Write("Auteur van het boek: ");
            string author = Console.ReadLine();

            Book bookToDelete = library.BookByTitleAuthor(title, author);
            if(bookToDelete == null)
            {
                Console.WriteLine("Boek niet gevonden.");
            }
            else
            {
                library.RemoveBook(bookToDelete);
                Console.WriteLine("Boek succesvol verwijderd uit de bibliotheek.");
            }

            Continue();
        }

        /// <summary>
        /// Toon alle boeken in de biblitheek
        /// </summary>
        static void ShowAllBooks()
        {
            Console.Clear();

            Console.WriteLine("6. Toon alle boeken in de bibliotheek");
            Console.WriteLine();

            List<Book> books = library.Books;
            foreach(Book book in books)
            {
                Console.WriteLine($"Titel: {book.Title}, Auteur: {book.Author}");
            }

            Continue();
        }

        /// <summary>
        /// Methode die ik gebruik op het einde van alle opties in het keuze menu om de gebruikers ervaring aangenamer te maken en om repetitie van code te vermijden
        /// </summary>
        static void Continue()
        {
            Console.WriteLine();
            Console.WriteLine("Druk op 'ENTER' om verder te gaan.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
