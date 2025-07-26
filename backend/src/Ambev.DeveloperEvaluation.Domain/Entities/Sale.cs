using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa uma venda.
/// </summary>
public class Sale
{
    public Guid Id { get; private set; }
    public string Number { get; private set; }
    public DateTime Date { get; private set; }
    public Guid CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public Guid BranchId { get; private set; }
    public string BranchName { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public List<SaleItem> Items { get; private set; } = new();

    protected Sale() { }

    public Sale(Guid customerId, string customerName, Guid branchId, string branchName, List<SaleItem> items)
    {
        Id = Guid.NewGuid();
        Number = GenerateNumber();
        Date = DateTime.UtcNow;
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        IsCancelled = false;
        Items = items ?? new List<SaleItem>();
        CalculateTotalAmount();
    }

    private string GenerateNumber()
    {
        return DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("A venda já está cancelada.");
        IsCancelled = true;
    }

    public void AddItem(SaleItem item)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Não é possível adicionar itens a uma venda cancelada.");
        Items.Add(item);
        CalculateTotalAmount();
    }

    public void RemoveItem(Guid itemId)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Não é possível remover itens de uma venda cancelada.");
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            Items.Remove(item);
            CalculateTotalAmount();
        }
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(i => i.TotalAmount);
    }
}