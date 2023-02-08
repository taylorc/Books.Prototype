using AutoFixture.NUnit3;
using CleanArchitecture.Application.Books.Commands.CreateBook;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Books.Commands;

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