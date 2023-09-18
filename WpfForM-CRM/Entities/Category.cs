using System;

namespace WpfForM_CRM.Entities;
 
public class Category
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid ShopId { get; set; }

}