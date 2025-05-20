using MongoDB.Bson.Serialization.Attributes;
using System.Net.Mail;

namespace CaseService.API.CaseService.Domain.Entities
{

    public class Case
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; } = default!;
        public required string Description { get; set; } = default!;
        public required string Speciality { get; set; } = default!;
        public string Status { get; set; } = "Submitted";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        private Case() { }

        #region Factory
        public static Case Create(string email, string description, string speciality)
        {
            // 1) Basic null/empty checks
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email is required.");
            if (string.IsNullOrWhiteSpace(description))
                throw new Exception("Description is required.");
            if (string.IsNullOrWhiteSpace(speciality))
                throw new Exception("Speciality is required.");

            try
            {
                var addr = new MailAddress(email.Trim());
                email = addr.Address; // normalizes the address
            }
            catch
            {
                throw new Exception("Email format is invalid.");
            }

            return new Case
            {
                Id = Guid.NewGuid(),
                Email = email,
                Description = description,
                Speciality = speciality
            };
        }

        #endregion

        #region Status Transitions
        public void MoveToInReview()
        {
            if (Status != "Submitted")
                throw new Exception("Can only move submitted cases to InReview.");
            Status = "InReview";
        }

        public void Finish()
        {
            if (Status != "InReview")
                throw new Exception("Can only finish cases that are InReview.");
            Status = "Finished";
        }
        #endregion
    }

}
