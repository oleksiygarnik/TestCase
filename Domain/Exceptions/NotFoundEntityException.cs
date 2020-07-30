using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public sealed class NotFoundEntityException : Exception
    {
        public NotFoundEntityException(string name, int id)
            : base($"Entity {name} with id ({id}) was not found.")
        {
        }
        public NotFoundEntityException(string name) : base($"Entity {name} was not found.") { }
    }
}
