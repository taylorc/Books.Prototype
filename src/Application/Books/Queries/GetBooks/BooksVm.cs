namespace Books.Prototype.Application.Books.Queries.GetBooks;

public class BooksVm
{

    public IList<BooksDto> Items { get; set; } = new List<BooksDto>();
}
