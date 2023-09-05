﻿using System;

namespace WpfForM_CRM.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public string Password { get; set; } = null!;
    public bool? RememberMe { get; set; }
    
}