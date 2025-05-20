using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Domain.ValueObjects;

public sealed class Email
{

    private static readonly Regex _rx =
    new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    private Email(string value) => Value =value;

    public string Value { get; set; }

    public override string ToString() => Value;

    public static Result<Email> Create (string? value)
    {
        if(string.IsNullOrEmpty(value) || !_rx.IsMatch(value))
        {
            return Result<Email>.Failure(DomainErrors.InvalidEmail);
        }

        return Result<Email>.Success(new Email(value));

    }

}
