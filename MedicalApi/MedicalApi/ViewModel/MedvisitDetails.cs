namespace MedicalApi.ViewModel
{
    public class MedvisitDetails
    {

        public string Date { get; set; } = null!;

        public string Illness { get; set; } = null!;

        public long BloodPressure { get; set; }

        public string Prescription { get; set; } = null!;
    }
}
