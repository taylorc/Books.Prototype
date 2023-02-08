using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Books.Commands.UpdateBook;

public record UpdateBookCommand : IRequest
{
    public int Id { get; init; }

    public string? Author { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public decimal Price { get; set; }

    public DateTime PublishDate { get; set; }

    public string? Description { get; set; }
}

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBookCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Books
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }
        
        if(!string.IsNullOrEmpty(request.Title))
            entity.Title = request.Title;

        if (!string.IsNullOrEmpty(request.Author))
            entity.Author = request.Author;

        if(!string.IsNullOrEmpty(request.Description))
            entity.Description = request.Description;

        if (!string.IsNullOrEmpty(request.Genre))
            entity.Genre = request.Genre;

        if (entity.Price != 0)
            entity.Price = request.Price;

        if (entity.PublishDate != DateTime.MinValue)
            entity.PublishDate = request.PublishDate;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
