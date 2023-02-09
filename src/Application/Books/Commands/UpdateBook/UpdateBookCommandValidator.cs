using Books.Prototype.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Books.Prototype.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBookCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(UpdateBookCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.Books
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
