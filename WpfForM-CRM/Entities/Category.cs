using System;
using System.ComponentModel.DataAnnotations;

namespace WpfForM_CRM.Entities;
 
public class Category
{
    [Key]
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime CreatedDate { get; set; }

    public Guid? ShopId { get; set; }
    public Shop? Shop { get; set; }
}