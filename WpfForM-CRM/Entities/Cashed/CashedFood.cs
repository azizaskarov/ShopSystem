using System.ComponentModel.DataAnnotations;
using System;

namespace WpfForM_CRM.Entities.Cashed;

public class CashedFood
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public double? Price { get; set; }
    public int? Count { get; set; }
    public string? Barcode { get; set; }
    public string? TabName { get; set; }
}