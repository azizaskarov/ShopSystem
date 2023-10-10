using System.ComponentModel.DataAnnotations;
using System;

namespace WpfForM_CRM.Entities;

public class CashedProduct
{
    [Key]
    public Guid Id { get; set; }

    public Guid? CategoryId { get; set; }

    public string? Name { get; set; }
    public string? Barcode { get; set; }
    public double? OriginalPrice { get; set; }
    public double? SellingPrice { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? CashedTime { get; set; }
    public Guid? ShopId { get; set; }
    public int? Count { get; set; }
    public int? TotalCount { get; set; }
}