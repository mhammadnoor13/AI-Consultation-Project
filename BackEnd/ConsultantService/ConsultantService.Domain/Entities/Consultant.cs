namespace ConsultantService.ConsultantService.Domain.Entities
{
    public class Consultant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string Email {  get; set; }
        public int CurrentLoad { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public static Consultant Create(
            string firstName,
            string lastName,
            string specialty,
            string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.", nameof(lastName));
            if (string.IsNullOrWhiteSpace(specialty))
                throw new ArgumentException("Specialty is required.", nameof(specialty));
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Valid email is required.", nameof(email));

            return new Consultant
            {
                Id = Guid.NewGuid(),
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                Specialty = specialty.Trim(),
                Email = email.Trim(),
                CurrentLoad = 0,
                CreatedAt = DateTime.UtcNow
            };
        }



    }
}
