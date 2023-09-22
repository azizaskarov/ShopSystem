using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WpfForM_CRM.Entities;

public class Shop
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public List<Category>? Categories { get; set; }
}