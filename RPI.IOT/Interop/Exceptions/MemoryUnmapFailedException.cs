using System;
using System.Runtime.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace RPI.IOT.Interop
{
    [Serializable]
    public class MemoryUnmapFailedException : Exception {
        public MemoryUnmapFailedException() {}
        public MemoryUnmapFailedException(string message) : base(message) {}
        public MemoryUnmapFailedException(string message, Exception innerException) : base(message, innerException) {}
        protected MemoryUnmapFailedException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}