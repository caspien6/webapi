using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Webstore.OwnExceptions
{
    public class EntityNotFoundException : Exception, ISerializable
    {
        public EntityNotFoundException(string message) : base($"Entity not found: {message}")
        {
        }
    }
}
