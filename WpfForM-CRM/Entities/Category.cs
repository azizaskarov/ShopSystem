using System;
using System.ComponentModel.DataAnnotations;

namespace WpfForM_CRM.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Guid? ShopId { get; set; }
    public Shop? Shop { get; set; }

}