using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consultants.DTOs;
public sealed record ConsultantResponse(Guid Id, string FullName, string Speciality);
