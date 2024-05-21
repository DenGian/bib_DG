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

            DateTime currentTime = DateTime.Now;

            // Boeken van het csv bestand toevoegen aan de bib
            List<Book> mybooks = Book.DeserializeBooksFromCsv("./csv/books.csv");
            foreach(Book book in mybooks)
            {
                library.AddBook(book);
            }

            // kranten van het csv bestand toevoegen aan de leeszaal
            Thread.Sleep(1000);
            List<NewsPaper> myNewspapers = NewsPaper.DeserializeNewspapersFromCsv("./csv/newspapers.csv");
            foreach (NewsPaper newspaper in myNewspapers)
            {
                library.AllReadingRoom.Add(currentTime, newspaper);
                currentTime = currentTime.AddTicks(1);
            }

            // Maandbladen van het csv bestand toevoegen aan de leeszaal
            Thread.Sleep(1000);
            List<Magazine> myMagazines = Magazine.DeserializeMagazinesFromCsv("./csv/magazines.csv");
            foreach (Magazine magazine in myMagazines)
            {
                library.AllReadingRoom.Add(currentTime, magazine);
                currentTime = currentTime.AddTicks(1);
            }

            Thread.Sleep(3000);

            Console.Clear();


            // Menu
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Keuzemenu:");
                Console.WriteLine("1.  Voeg een boek toe");
                Console.WriteLine("2.  Voeg informatie toe aan een boek (ISBN, Genre, Uitgeverij)");
                Console.WriteLine("3.  Toon alle info van een boek (via titel en auteur)");
                Console.WriteLine("4.  Zoek een boek");
                Console.WriteLine("5.  Verwijder een boek");
                Console.WriteLine("6.  Toon alle boeken in de bibliotheek");
                Console.WriteLine("7.  Voeg een item toe aan de leeszaal");
                Console.WriteLine("8.  Toon alle kranten");
                Console.WriteLine("9.  Toon alle maandbladen");
                Console.WriteLine("10. Toon alle aanwinsten van de leeszaal van vandaag");
                Console.WriteLine("11. Ontleen een boek");
                Console.WriteLine("12. Breng een boek terug");
                Console.WriteLine("13. Exit");
                Console.Write("\nSelecteer een optie [1-13]: ");

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
                        AddItemReadingRoom();
                        break;
                    case "8":
                        ShowAllNewspapers();
                        break;
                    case "9":
                        ShowAllMagazines();
                        break;
                    case "10":
                        AcquisitionsReadingRoomToday();
                        break;
                    case "11":
                        BorrowBook();
                        break;
                    case "12":
                        ReturnBook();
                        break;
                    case "13":
                        exit = true;
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
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

            try
            {
                Book book = new Book(title, author, library);
                library.AddBook(book);

                Console.WriteLine();
                Console.WriteLine("Boek succesvol toegevoegd aan de bibliotheek.");
            }
            catch(InvalidTitleException itex)
            {
                Console.WriteLine("Fout bij het toevoegen van een boek: " + itex.ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine("Er is een fout opgetreden bij het toevoegen van een boek: " + e.Message);
            }

            Continue();
        }

        /// <summary>
        /// Voeg informatie aan een boek toe aan de hand van ISBN, genre of uitgeverij
        /// </summary>
        static void AddBookInfo()
        {
            Console.Clear();

            Console.WriteLine("2. Voeg informatie toe aan een boek (ISBN, Genre, Uitgeverij)\n");

            Console.Write("Titel van het boek: ");
            string title = Console.ReadLine();
            Console.Write("Auteur van het boek: ");
            string author = Console.ReadLine();

            Book book = library.BookByTitleAuthor(title, author);

            if(book == null)
            {
                Console.WriteLine("\nBoek niet gevonden.");
                Continue();
                return;
            }

            Console.WriteLine("\nWelke informatie wil je toevoegen?\n");
            Console.WriteLine("1. ISBN");
            Console.WriteLine("2. Genre");
            Console.WriteLine("3. Uitgeverij");
            Console.Write("Selecteer een optie [1-3]: ");
            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    try
                    {
                        Book.AddISBN(book);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Er is een fout opgetreden bij het toevoegen van ISBN: " + e.Message);
                    }
                    break;
                case "2":
                    try
                    {
                        Book.AddGenre(book);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Er is een fout opgetreden bij het toevoegen van Genre: " + e.Message);
                    }
                    break;
                case "3":
                    try
                    {
                        Book.AddPublisher(book);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Er is een fout opgetreden bij het toevoegen van Uitgeverij: " + e.Message);
                    }
                    break;
                default:
                    Console.WriteLine("\nOngeldige keuze!");
                    break;
            }

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

            Console.WriteLine("\nInformatie van het gevonden boek:");
            Console.WriteLine($"- Titel: {book.Title}");

            if(!string.IsNullOrEmpty(book.ISBN))
            {
                Console.WriteLine($"- ISBN: {book.ISBN}");
            }
            else
            {
                Console.WriteLine($"- ISBN: Niet beschikbaar");
            }

            Console.WriteLine($"- Auteur: {book.Author}");

            if(book.ReleaseDate != DateTime.MinValue)
            {
                Console.WriteLine($"- Datum van uitgave: {book.ReleaseDate.ToString("dd-MM-yyyy")}");
            }
            else
            {
                Console.WriteLine($"- Datum van uitgave: Niet beschikbaar");
            }

            if(book.NumberOfPages != 0)
            {
                Console.WriteLine($"- Aantal pagina's: {book.NumberOfPages}");
            }
            else
            {
                Console.WriteLine($"- Aantal pagina's: Niet beschikbaar");
            }

            if(!string.IsNullOrEmpty(book.BookGenre.ToString()))
            {
                Console.WriteLine($"- Genre: {book.BookGenre}");
            }
            else
            {
                Console.WriteLine($"- Genre: Niet beschikbaar");
            }

            if(!string.IsNullOrEmpty(book.Publisher))
            {
                Console.WriteLine($"- Uitgeverij: {book.Publisher}");
            }
            else
            {
                Console.WriteLine($"- Uitgeverij: Niet beschikbaar");
            }

            if(book.Price != 0)
            {
                Console.WriteLine($"- Prijs: {book.Price}");
            }
            else
            {
                Console.WriteLine($"- Prijs: Niet beschikbaar");
            }

            Continue();
        }

        /// <summary>
        /// Keuzemenu om een boek / boeken te zoeken op verschillende manieren
        /// </summary>
        static void SearchBook()
        {
        
            Console.Clear();
            
            Console.WriteLine("4. Zoek een boek\n");
            Console.WriteLine("1. Zoek op ISBN");
            Console.WriteLine("2. Zoek op uitgever");
            Console.Write("Selecteer een zoekoptie [1-2]: ");
            
            string choice = Console.ReadLine();
            switch(choice)
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

            Continue();
        }
        
        /// <summary>
        /// Zoek een boek op basis van het ISBN
        /// </summary>
        static void SearchBookByISBN()
        {
            Console.Write("\nVoer het ISBN van het boek in: ");
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

            Console.WriteLine("6. Toon alle boeken in de bibliotheek\n");

            List<Book> books = library.Books;
            foreach(Book book in books)
            {
                Console.WriteLine($"Titel: {book.Title}, Auteur: {book.Author}");
            }

            Continue();
        }

        static void AddItemReadingRoom()
        {
            Console.Clear();
            Console.WriteLine("7. Voeg een item toe aan de leeszaal\n");
            Console.WriteLine("1. Voeg een krant toe");
            Console.WriteLine("2. Voeg een maandblad toe");
            Console.Write("Selecteer een optie [1-2]: ");
            string readingRoomChoice = Console.ReadLine();
            switch(readingRoomChoice)
            {
                case "1":
                    AddNewsPaper();
                    break;
                case "2":
                    AddMagazine();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ongeldige keuze!");
                    break;
            }
        }

        /// <summary>
        /// Methode om een krant aan de ReadingRoom toe te voegen
        /// </summary>
        static void AddNewsPaper()
        {
            Console.Clear();

            Console.WriteLine("7. Voeg een krant toe aan de leeszaal\n");

            Console.WriteLine("Wat is de naam van de krant?");
            string nameNewsPaper = Console.ReadLine();

            Console.WriteLine("Wat is de datum van de krant?");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Wat is de uitgeverij van de krant?");
            string publisher = Console.ReadLine();

            NewsPaper newsPaper = new NewsPaper(nameNewsPaper, publisher, date);

            library.AllReadingRoom.Add(DateTime.Now, newsPaper);

            Console.WriteLine("\nKrant succesvol toegevoegd aan de leeszaal");

            Continue();
        }

        /// <summary>
        /// Methode om een maandblad aan de ReadingRoom toe te voegen
        /// </summary>
        static void AddMagazine()
        {
            Console.Clear();

            Console.WriteLine("7. Voeg een maandblad toe aan de leeszaal\n");

            Console.WriteLine("Wat is de naam van het maandblad?");
            string nameMagazine = Console.ReadLine();

            Console.WriteLine("Wat is de maand van het maandblad?");
            byte month = byte.Parse(Console.ReadLine());

            Console.WriteLine("Wat is het jaar van het maandblad?");
            uint year = uint.Parse(Console.ReadLine());

            Console.WriteLine("Wat is de uitgeverij van het maandblad?");
            string publisher = Console.ReadLine();

            Magazine magazine = new Magazine(nameMagazine, publisher, month, year);

            library.AllReadingRoom.Add(DateTime.Now, magazine);

            Console.WriteLine("\nMaandblad succesvol toegevoegd aan de leeszaal");

            Continue();
        }

        /// <summary>
        /// Methode om alle kranten te tonen
        /// </summary>
        static void ShowAllNewspapers()
        {
            Console.Clear();

            Console.WriteLine("8. Toon alle kranten\n");

            Console.WriteLine("De kranten in de leeszaal:");

            Dictionary<DateTime, ReadingRoomItem> newspapers = library.AllReadingRoom;
            foreach (KeyValuePair<DateTime, ReadingRoomItem> item in newspapers)
            {
                if (item.Value is NewsPaper)
                {
                    NewsPaper newspaper = (NewsPaper)item.Value;
                    string formattedDate = newspaper.Date.ToString("dddd d MMMM yyyy");
                    Console.WriteLine($"- {newspaper.Title} van {formattedDate} van uitgevrij {newspaper.Publisher}");
                }
            }

            Continue();
        }


        /// <summary>
        /// Methode om alle maandbladen te zien
        /// </summary>
        static void ShowAllMagazines()
        {
            Console.Clear();

            Console.WriteLine("9. Toon alle maandbladen\n");

            Console.WriteLine("Alle maandbladen uit de leeszaal:");

            Dictionary<DateTime, ReadingRoomItem> magazines = library.AllReadingRoom;
            foreach (KeyValuePair<DateTime, ReadingRoomItem> item in magazines)
            {
                if(item.Value is Magazine)
                {
                    Magazine magazine = (Magazine)item.Value;
                    string formattedDate = $"{magazine.Month:D2}/{magazine.Year}";
                    Console.WriteLine($"- {magazine.Title} van {formattedDate} van uitgeverij {magazine.Publisher}");
                }
            }

            Continue();
        }

        /// <summary>
        /// Methode om alle aanwinsten van de leeszaal van vandaag te tonen
        /// </summary>
        static void AcquisitionsReadingRoomToday()
        {
            Console.Clear();
            Console.WriteLine("10. Toon alle aanwinsten van de leeszaal van vandaag\n");

            DateTime today = DateTime.Today;
            Console.WriteLine($"Aanwinsten in de leeszaal van {today.Date.ToString("dddd d MMMM yyyy")}\n");

            Dictionary<DateTime, ReadingRoomItem> acquisitions = library.AllReadingRoom;

            List<string> newspapers = new List<string>();
            List<string> magazines = new List<string>();

            foreach (KeyValuePair<DateTime, ReadingRoomItem> item in acquisitions)
            {
                if (item.Key.Date == today)
                {
                    if (item.Value is NewsPaper newspaper)
                    {
                        newspapers.Add($"- {newspaper.Title}, met id {newspaper.Identification}");
                    }
                    else if (item.Value is Magazine magazine)
                    {
                        magazines.Add($"- {magazine.Title}, met id {magazine.Identification}");
                    }
                }
            }

            if (newspapers.Count > 0)
            {
                Console.WriteLine("Kranten:");
                foreach (var newspaper in newspapers)
                {
                    Console.WriteLine(newspaper);
                }
            }

            if(magazines.Count > 0)
            {
                Console.WriteLine("\nMaandbladen:");
                foreach (var magazine in magazines)
                {
                    Console.WriteLine(magazine);
                }
            }

            Continue();
        }
        /// <summary>
        /// Methode om boeken te kunnen ontlenen
        /// </summary>
        static void BorrowBook()
        {
            Console.Clear();
            Console.WriteLine("11. Ontleen een boek\n");

            Console.Write("Voer de titel van het boek in: ");
            string title = Console.ReadLine();
            Console.Write("Voer de auteur van het boek in: ");
            string author = Console.ReadLine();

            Book book = library.BookByTitleAuthor(title, author);

            if(book == null)
            {
                Console.WriteLine("\nBoek niet gevonden.");
                Continue();
                return;
            }

            book.Borrow();

            Continue();
        }
        /// <summary>
        /// Methode om een boek terug te brengen
        /// </summary>
        static void ReturnBook()
        {
            Console.Clear();
            Console.WriteLine("12. Breng een boek terug\n");

            Console.Write("Voer de titel van het boek in: ");
            string title = Console.ReadLine();
            Console.Write("Voer de auteur van het boek in: ");
            string author = Console.ReadLine();

            Book book = library.BookByTitleAuthor(title, author);

            if(book == null)
            {
                Console.WriteLine("\nBoek niet gevonden.");
                Continue();
                return;
            }

            book.Return();

            Continue();
        }


        /// <summary>
        /// Methode die ik gebruik op het einde van alle opties in het keuze menu om de gebruikers ervaring aangenamer te maken en om repetitie van code te vermijden
        /// </summary>
        static void Continue()
        {
            Console.WriteLine("\nDruk op 'ENTER' om verder te gaan.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
