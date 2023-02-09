using AutoFixture.NUnit3;
using Books.Prototype.Application.Common.Exceptions;
using Books.Prototype.Application.Books.Commands.CreateBook;
using Books.Prototype.Application.Books.Commands.UpdateBook;
using Books.Prototype.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Books.Prototype.Application.IntegrationTests.Books.Commands;

using static Testing;

public class UpdateBookTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBookId()
    {
        var command = new UpdateBookCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test,AutoData]
    public async Task ShouldUpdateBook(CreateBookCommand createBookCommand)
    {
        var userId = await RunAsDefaultUserAsync();

        var itemId = await SendAsync(createBookCommand);

        var command = new UpdateBookCommand
        {
            Id = itemId,
            Title = "Updated Item Title",
            Description = "Updated Description"
        };

        await SendAsync(command);

        var item = await FindAsync<Book>(itemId);

        item.Should().NotBeNull();
        item!.Title.Should().Be(command.Title);
    }
}
