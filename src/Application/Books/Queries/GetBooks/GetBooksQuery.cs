using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Books.Queries.GetBooks;

[Authorize]
public record GetBooksQuery : IRequest<BooksVm>;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, BooksVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BooksVm> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return new BooksVm
        {
            Lists = await _context.Books
                .AsNoTracking()
                .ProjectTo<BooksDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
