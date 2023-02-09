using Books.Prototype.Application.Common.Interfaces;

namespace Books.Prototype.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
