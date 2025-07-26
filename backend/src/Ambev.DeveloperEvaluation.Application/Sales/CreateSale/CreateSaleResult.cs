namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Representa a resposta retornada após a criação bem-sucedida de uma nova venda.
/// </summary>
/// <remarks>
/// Esta resposta contém o identificador único da venda criada,
/// que pode ser utilizado para operações ou referências futuras.
/// </remarks>
public class CreateSaleResult
{
    /// <summary>
    /// Obtém ou define o identificador único da venda criada.
    /// </summary>
    /// <value>Um GUID que identifica unicamente a venda criada no sistema.</value>
    public Guid Id { get; set; }
}
