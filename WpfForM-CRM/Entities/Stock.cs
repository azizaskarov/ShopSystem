using System;

namespace WpfForM_CRM.Entities;

public class Stock
{
    
    //public  string? Name { get; set; }
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