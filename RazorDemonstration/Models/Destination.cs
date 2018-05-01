using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RazorDemonstration.Models
{
    public abstract class Destination
    {
        public abstract int Insert(IDbConnection connection);
    }
}
