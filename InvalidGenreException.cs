namespace bib_ian_mondelaers;

class InvalidGenreException : ApplicationException
{
    public override string ToString()
    {
        return "Dit is een ongeldig genre.";
    }
}
