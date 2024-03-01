using System;
using System.Collections.Generic;
using bib_ian_mondelaers;

public class Program
{
    static void Main()
    {
        List<Book> mybooks = Book.DeserializeBooksFromCsv("./csv/books.csv");
        foreach (Book book in mybooks)
        {
            book.AllBookInfo();
            Console.WriteLine();
        }
    }
}