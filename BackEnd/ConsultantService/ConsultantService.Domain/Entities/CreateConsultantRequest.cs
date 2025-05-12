namespace ConsultantService.ConsultantService.Domain.Entities
{
    public class CreateConsultantRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

}
