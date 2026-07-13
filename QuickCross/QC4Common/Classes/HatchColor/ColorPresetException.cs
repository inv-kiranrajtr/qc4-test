using System;
using System.Runtime.Serialization;

namespace QC4Common.Classes.HatchColor
{
    /// <summary>
    /// Represents an exception thrown when there is an issue related to color presets.
    /// </summary>
    [Serializable]
    public class ColorPresetException : Exception, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPresetException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        public ColorPresetException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPresetException"/> class with a specified error message and optional format arguments.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        /// <param name="args">An array of objects to format into the error message.</param>
        public ColorPresetException(string message, params object[] args) : base(string.Format(message, args)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPresetException"/> class with a specified error message and inner exception.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public ColorPresetException(string message, Exception innerException) : base(message, innerException) { }

        // Implement ISerializable
        protected ColorPresetException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize the base class data
            base.GetObjectData(info, context);
        }
    }
}