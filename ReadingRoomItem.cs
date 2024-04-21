namespace bib_ian_mondelaers;

internal abstract class ReadingRoomItem
{
    // Eigenschappen
    private string title;
    public string Title
    {
        get { return title; }
    }
    private string publisher;
    public string Publisher
    {
        get { return publisher; }
        set { publisher = value; }
    }
    public abstract string Identification { get; }
    public abstract string Categorie { get; }

    // Constructors
    public ReadingRoomItem(string title, string publisher)
    {
        this.title = title;
        this.publisher = publisher;
    }
}
