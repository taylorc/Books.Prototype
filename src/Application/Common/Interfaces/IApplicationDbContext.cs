using Books.Prototype.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.Prototype.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
