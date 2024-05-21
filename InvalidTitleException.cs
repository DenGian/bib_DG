namespace bib_ian_mondelaers;

class InvalidTitleException: ApplicationException
{
    public override string ToString()
    {
        return "Titel mag niet leeg zijn.";
    }
}
