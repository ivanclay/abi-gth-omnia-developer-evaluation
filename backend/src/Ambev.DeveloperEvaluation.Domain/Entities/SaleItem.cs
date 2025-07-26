namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa um item de venda.
/// </summary>
public class SaleItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    protected SaleItem() { }

    public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        IsCancelled = false;
        CalculateTotalAmount();
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("O item já está cancelado.");
        IsCancelled = true;
    }

    private void CalculateTotalAmount()
    {
        var subtotal = Quantity * UnitPrice;
        TotalAmount = subtotal - (subtotal * Discount);
    }
}