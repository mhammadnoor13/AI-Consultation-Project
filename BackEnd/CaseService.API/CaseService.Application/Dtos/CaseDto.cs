namespace CaseService.API.CaseService.Application.Dtos
{
    public class CaseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Speciality { get; set; } = default!;
        public string Status { get; set; } = "Submitted";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public CaseDto(Guid id, string email, string decription, string speciality, string status, DateTime createdAt)
        {
            Id = id;
            Email = email;
            Description = decription;
            Speciality = speciality;
            Status = status;
            CreatedAt = createdAt;



        }
    }
}
