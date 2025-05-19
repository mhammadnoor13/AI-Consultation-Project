using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consultants.Commands.CreateConsultant;

public sealed record CreateConsultantCmd(string FirstName,string LastName,string Email, string Speciality):IRequest<Result<Guid>>;
