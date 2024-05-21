namespace bib_ian_mondelaers;

class InvalidPublisherException : ApplicationException
{
    public override string ToString()
    {
        return "Dit is een ongeldige uitgeverij.";
    }
}
