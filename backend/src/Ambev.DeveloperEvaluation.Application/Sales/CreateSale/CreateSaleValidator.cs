using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator para CreateSaleCommand que define as regras de validação para o comando de criação de venda.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Inicializa uma nova instância de CreateSaleCommandValidator com as regras de validação definidas.
    /// </summary>
    /// <remarks>
    /// Regras de validação incluem:
    /// - CustomerId: Obrigatório
    /// - CustomerName: Obrigatório, entre 3 e 100 caracteres
    /// - BranchId: Obrigatório
    /// - BranchName: Obrigatório, entre 3 e 100 caracteres
    /// - Items: Deve conter pelo menos 1 item
    /// - Validação de cada item via CreateSaleItemCommandValidator
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("O cliente é obrigatório.");

        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
            .Length(3, 100).WithMessage("O nome do cliente deve ter entre 3 e 100 caracteres.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("A filial é obrigatória.");

        RuleFor(sale => sale.BranchName)
            .NotEmpty().WithMessage("O nome da filial é obrigatório.")
            .Length(3, 100).WithMessage("O nome da filial deve ter entre 3 e 100 caracteres.");

        RuleFor(sale => sale.Items)
            .NotNull().WithMessage("Itens da venda são obrigatórios.")
            .Must(items => items != null && items.Count > 0).WithMessage("A venda deve conter pelo menos um item.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new CreateSaleItemCommandValidator());
    }
}

/// <summary>
/// Validator para itens do comando de criação de venda.
/// </summary>
public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemCommandValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("O produto é obrigatório.");

        RuleFor(item => item.ProductName)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.");

        RuleFor(item => item.Quantity)
            .InclusiveBetween(1, 20).WithMessage("A quantidade deve ser entre 1 e 20.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

        RuleFor(item => item.Discount)
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