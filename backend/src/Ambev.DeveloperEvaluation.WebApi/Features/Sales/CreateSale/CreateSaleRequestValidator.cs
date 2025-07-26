using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("O cliente é obrigatório.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
            .MinimumLength(3).WithMessage("O nome do cliente deve ter pelo menos 3 caracteres.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("A filial é obrigatória.");

        RuleFor(x => x.BranchName)
            .NotEmpty().WithMessage("O nome da filial é obrigatório.")
            .MinimumLength(3).WithMessage("O nome da filial deve ter pelo menos 3 caracteres.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Itens da venda são obrigatórios.")
            .Must(items => items != null && items.Count > 0).WithMessage("A venda deve conter pelo menos um item.");

        RuleForEach(x => x.Items).SetValidator(new SaleItemRequestValidator());
    }
}
