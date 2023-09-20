using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace WpfForM_CRM.Entities;


public class User
{
    [Key]
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public string Password { get; set; } = null!;
    public bool? RememberMe { get; set; }
    
    public List<Shop>? Shops { get; set; }
}