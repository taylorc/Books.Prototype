using Books.Prototype.Application.Common.Models;
using Books.Prototype.Application.Books.Commands.CreateBook;
using Books.Prototype.Application.Books.Commands.UpdateBook;
using Books.Prototype.Application.Books.Queries.GetBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Prototype.WebUI.Controllers;

public class BooksController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<BooksVm>> GetBooks()
    {
        var query = new GetBooksQuery();
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBookCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateBookCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}
