using System;
using System.ComponentModel.DataAnnotations;

namespace WpfForM_CRM.Entities;
 
public class Category
{
    [Key]
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public required Guid ShopId { get; set; }
}