namespace bib_ian_mondelaers;

class InvalidReleaseDateException: ApplicationException
{
    public override string ToString()
    {
        return "De datum van uitgave kan niet in de toekomst liggen.";
    }
}
