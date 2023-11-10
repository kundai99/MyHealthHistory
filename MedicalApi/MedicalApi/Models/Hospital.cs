using System;
using System.Collections.Generic;

namespace MedicalDatabase.Models;

public partial class Hospital
{
    public long HId { get; set; }

    public string HospitalName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public long? Type { get; set; }
}
