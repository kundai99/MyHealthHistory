using System;
using System.Collections.Generic;

namespace MedicalDatabase.Models;

public partial class Account
{
    public long AccId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Category { get; set; }
}
