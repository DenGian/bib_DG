namespace bib_ian_mondelaers;

class InvalidISBNException: ApplicationException
{
    public override string ToString()
    {
        return "Dit is een ongeldig ISBN.";
    }
}
