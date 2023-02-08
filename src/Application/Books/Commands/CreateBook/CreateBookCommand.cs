using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Books.Commands.CreateBook;

public record CreateBookCommand : IRequest<int>
{
    public string? Author { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public decimal Price { get; set; }

    public DateTime PublishDate { get; set; }

    public string? Description { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBookCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = new Book
            {
                Title = request.Title, Author = request.Author, Description = request.Description, Genre = request.Genre,
                Price = request.Price,
            PublishDate = request.PublishDate
            };

        _context.Books.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
