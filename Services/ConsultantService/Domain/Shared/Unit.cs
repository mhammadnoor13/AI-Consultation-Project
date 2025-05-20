using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public readonly struct Unit
    {
        // Used when no data is need
        public static Unit Value => new();
    }
}
