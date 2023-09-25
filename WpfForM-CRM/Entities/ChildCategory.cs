using System;
using System.ComponentModel.DataAnnotations;

namespace WpfForM_CRM.Entities;

public class ChildCategory
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid? ShopId { get; set; }
    public Shop? Shop { get; set; }

}