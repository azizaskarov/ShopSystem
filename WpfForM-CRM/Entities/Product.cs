using System;
using System.ComponentModel.DataAnnotations;

namespace WpfForM_CRM.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public double? OriginalPrice { get; set; }
    public double? SellingPrice { get; set; }
    public int? Count { get; set; }
    public string? Barcode { get; set; }

    public Guid? ChildCategoryId { get; set; }
    public ChildCategory? ChildCategory { get; set; }

    public Guid? ShopId { get; set; }
    public Guid? CategoryId { get; set;}
    public Guid? UserId { get; set; }
}

