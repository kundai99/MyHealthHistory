using System;
using System.Collections.Generic;

namespace MedicalDatabase.Models;

public partial class Doctor
{
    public long DId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string ProfessionalNum { get; set; } = null!;

    public string Speciality { get; set; } = null!;

    public long HId { get; set; }

    public long AccId { get; set; }
}
