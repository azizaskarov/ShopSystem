using System;

namespace WpfForM_CRM.Entities;

public class Shop
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Owner { get; set; }
}