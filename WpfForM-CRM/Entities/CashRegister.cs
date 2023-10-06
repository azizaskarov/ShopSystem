using System;

namespace WpfForM_CRM.Entities;

public class CashRegister
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? ShopId { get; set; }
}