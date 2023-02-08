using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Books.Queries.GetBooks;

public class BooksDto: IMapFrom<Book>
{
    public int Id { get; set; }

    //TODO clean this up. Maybe with mapping
    public string? BookId { get; }

    public string? Author { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public decimal Price { get; set; }

    public DateTime PublishDate { get; set; }

    public string? Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BooksDto>()
            .ForMember(x => x.BookId, opt => opt.MapFrom(x => $"B{x.Id}"));
    }
}
