using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace WpfForM_CRM.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Guid? UserId { get; set; }
    public Guid? ShopId { get; set; }
    public Shop? Shop { get; set; }

    public List<ChildCategory>? ChildCategories { get; set; }

}