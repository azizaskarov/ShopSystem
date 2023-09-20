using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace WpfForM_CRM.Entities;

public class Shop
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Owner { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }
}