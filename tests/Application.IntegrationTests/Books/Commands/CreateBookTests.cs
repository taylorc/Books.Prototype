using AutoFixture.NUnit3;
using Books.Prototype.Application.Books.Commands.CreateBook;
using Books.Prototype.Application.Common.Exceptions;
using Books.Prototype.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Books.Prototype.Application.IntegrationTests.Books.Commands;

using static Testing;

public class CreateBookTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateBookCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test, AutoData]
    public async Task ShouldCreateBook(CreateBookCommand command)
    {
        var userId = await RunAsDefaultUserAsync();

        var itemId = await SendAsync(command);

        var item = await FindAsync<Book>(itemId);

        item.Should().NotBeNull();
        item!.Author.Should().Be(command.Author);
        item.Title.Should().Be(command.Title);
        item.Description.Should().Be(command.Description);
        item.Genre.Should().Be(command.Genre);
        item.Price.Should().Be(command.Price);
        item.PublishDate.Should().Be(command.PublishDate);
    }
}