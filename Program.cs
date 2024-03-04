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

            // Menu
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Keuzemenu:");
                Console.WriteLine("1. Voeg een boek toe");
                Console.WriteLine("2. Voeg informatie toe aan een boek");
                Console.WriteLine("3. Toon alle info van een boek (O.B.V Auteur/Titel)");
                Console.WriteLine("4. Zoek een boek");
                Console.WriteLine("5. Verwijder een boek");
                Console.WriteLine("6. Toon alle boeken in de bibliotheek");
                Console.WriteLine("7. Exit");
                Console.Write("Selecteer een optie: ");

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
                Console.WriteLine();
            }
        }

        //Methoden
        static void AddBook()
        {
            Console.Write("Voer de titel van het boek in: ");
            string title = Console.ReadLine();
            Console.Write("Voer de auteur van het boek in: ");
            string author = Console.ReadLine();

            Book book = new Book(title, author, library);
            library.AddBook(book);

            Console.WriteLine("Boek toegevoegd aan de bibliotheek.");
        }

        static void AddBookInfo()
        {
            
        }

        static void ShowBookInfo()
        {
            
        }

        static void SearchBook()
        {
            
        }

        static void DeleteBook()
        {
            
        }

        static void ShowAllBooks()
        {
            List<Book> books = library.Books;
            foreach (Book book in books)
            {
                Console.WriteLine($"Titel: {book.Title}, Auteur: {book.Author}");
            }
        }
    }
}
