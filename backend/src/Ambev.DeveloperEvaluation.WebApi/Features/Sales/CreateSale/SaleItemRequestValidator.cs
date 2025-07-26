using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class SaleItemRequestValidator : AbstractValidator<SaleItemRequest>
{
    public SaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O produto é obrigatório.");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.");

        RuleFor(x => x.Quantity)
            .InclusiveBetween(1, 20).WithMessage("A quantidade deve ser entre 1 e 20.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

        RuleFor(x => x.Quantity)
            .Must(q => q < 4 || q >= 4 && q < 10 || q >= 10 && q <= 20)
            .WithMessage("Quantidade inválida para desconto.");

        RuleFor(x => x.Discount)
            .Must((item, discount) =>
            {
                if (item.Quantity < 4) return discount == 0;
                if (item.Quantity >= 4 && item.Quantity < 10) return discount == 0.10m;
                if (item.Quantity >= 10 && item.Quantity <= 20) return discount == 0.20m;
                return false;
            })
            .WithMessage("Desconto inválido para a quantidade informada.");
    }
}
