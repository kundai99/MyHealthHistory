using System;
using System.Collections.Generic;

namespace MedicalDatabase.Models;

public partial class MedicalVisit
{
    public long MId { get; set; }

    public long PId { get; set; }

    public string Date { get; set; } = null!;

    public long BloodPressure { get; set; }

    public long HId { get; set; }

    public string Stay { get; set; } = null!;

    public long DId { get; set; }

    public string? PatientIdnumber { get; set; }

    public long? PatientTemperature { get; set; }

    public long? PatientWeight { get; set; }

    public string Symptoms { get; set; } = null!;

    public string Diagnosis { get; set; } = null!;

    public string Prescription { get; set; } = null!;
}
