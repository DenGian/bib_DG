namespace bib_ian_mondelaers;

class InvalidPriceException: ApplicationException
{
    public override string ToString()
    {
        return "Prijs kan niet negatief zijn.";
    }
}
