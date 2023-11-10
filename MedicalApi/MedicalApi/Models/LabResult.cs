using System;
using System.Collections.Generic;

namespace MedicalDatabase.Models;

public partial class LabResult
{
    public long LbId { get; set; }

    public long MId { get; set; }

    public string DocumentName { get; set; } = null!;

    public string DocumentPath { get; set; } = null!;
}
