using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command para criar uma nova venda.
/// </summary>
/// <remarks>
/// Este comando captura os dados necessários para criar uma venda,
/// incluindo cliente, filial e itens da venda.
/// Implementa <see cref="IRequest{TResponse}"/> para iniciar a requisição
/// que retorna um <see cref="CreateSaleResult"/>.
/// 
/// Os dados fornecidos neste comando são validados usando o
/// <see cref="CreateSaleCommandValidator"/>, que estende
/// <see cref="AbstractValidator{T}"/> para garantir que os campos estejam
/// corretamente preenchidos e sigam as regras de negócio.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Id do cliente.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Nome do cliente.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Id da filial.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Nome da filial.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Lista de itens da venda.
    /// </summary>
    public List<CreateSaleItemCommand> Items { get; set; } = new();

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Representa um item do comando de criação de venda.
/// </summary>
public class CreateSaleItemCommand
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
