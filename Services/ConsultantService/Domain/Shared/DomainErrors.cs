using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public static class DomainErrors
    {
        public const string EmptyFirstName  = "First name must not be blank !";
        public const string EmptyLastName  = "Last name must not be blank !";
        public const string InvalidEmail  = "Email is invalid !";
        public const string EmptySpeciality  = "Consultant speciality must not be blank !";

    }
}
