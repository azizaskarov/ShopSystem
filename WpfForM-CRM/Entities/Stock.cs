using System;
using System.ComponentModel;

namespace WpfForM_CRM.Entities;

public class Stock
{
    
    public int Number { get; set; } 
    public string? Barcode { get; set; }
    public string? Category { get; set; }
    public string? ChildCategory { get; set; }
    public string? ProductName { get; set; }
    public string? OriginalPrice { get; set; }
    public string? SellingPrice { get; set; }
    public string Count { get; set; }
    //public string? ShopName { get; set; }
}