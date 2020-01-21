using System;
using System.Runtime.Serialization;

namespace DecoratorDependencyInjection.Core
{
    [Serializable]
    internal class OutOfCoffeeException : Exception
    {
        public OutOfCoffeeException()
        {
        }

        public OutOfCoffeeException(string message) : base(message)
        {
        }

        public OutOfCoffeeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutOfCoffeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}