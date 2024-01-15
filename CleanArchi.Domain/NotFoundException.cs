using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchi.Domain
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} with key {key} not found.") { }
    }
}
