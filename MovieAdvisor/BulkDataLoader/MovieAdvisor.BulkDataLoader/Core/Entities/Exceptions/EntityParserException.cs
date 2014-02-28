namespace MovieAdvisor.BulkDataLoader.Core.Entities.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class EntityParserException : Exception
    {
        public EntityParserException()
        {
        }

        public EntityParserException(string message)
            : base(message)
        {
        }

        public EntityParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityParserException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected EntityParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}