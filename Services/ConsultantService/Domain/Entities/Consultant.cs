using Domain.Shared;
using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Consultant
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public Email Email { get; set; }
    public string Speciality { get; set; }
    [BsonRepresentation(BsonType.String)]
    public List<Guid> CasesAssigned { get; set; } = new();

    private Consultant() { }

    public static Result<Consultant> Create(
        string? firstName,
        string? lastName,
        string? email,
        string? speciality
        )
    {
        return ValidateName(firstName, DomainErrors.EmptyFirstName)
            .Bind(fn => ValidateName(lastName, DomainErrors.EmptyLastName)
            .Bind(ln => Email.Create(email)
            .Bind(em => ValidateName(speciality, DomainErrors.EmptySpeciality)
            .Bind(sp =>
                    {
                        var consultant = new Consultant
                        {
                            FirstName = fn,
                            LastName = ln,
                            Email = em,
                            Speciality = sp
                        };
                        return Result<Consultant>.Success(consultant);

                    }))));
        

    }


    // Helper

    private static Result<string> ValidateName (string? value , string error) 
        => string.IsNullOrEmpty(value) 
        ? Result<string>.Failure(error)
        : Result<string>.Success(value.Trim());






}
