namespace CleanArchitecture.Application.Books.Queries.GetBooks;

public class BooksVm
{

    public IList<BooksDto> Lists { get; set; } = new List<BooksDto>();
}
