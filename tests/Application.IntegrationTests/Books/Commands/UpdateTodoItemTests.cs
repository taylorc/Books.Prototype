using AutoFixture.NUnit3;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Books.Commands.CreateBook;
using CleanArchitecture.Application.Books.Commands.UpdateBook;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Books.Commands;

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
