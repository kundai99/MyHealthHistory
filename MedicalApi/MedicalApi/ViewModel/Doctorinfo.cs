namespace MedicalApi.ViewModel
{
    public class Doctorinfo
    {
        public long Did { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }

        public string ProfessionalNum { get; set; } = null!;

        public string Speciality { get; set; } = null!;
        public string HospitalName { get; set; }
    }
}
