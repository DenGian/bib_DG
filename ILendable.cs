namespace bib_ian_mondelaers;

internal interface ILendable
{
bool IsAvailable { get; set; }
DateTime BorrowingDate { get; set; }
int BorrowDays { get; set; }
void Borrow();
void Return();
}
