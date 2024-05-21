namespace bib_ian_mondelaers;

class InvalidPageCountException: ApplicationException
{
    public override string ToString()
    {
        return "Aantal pagina's moet een positief getal zijn.";
    }
}
