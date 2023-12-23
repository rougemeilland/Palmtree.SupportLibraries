using System;

namespace Palmtree.IO.Compression.Stream
{
    public class DataErrorException
        : Exception
    {
        public DataErrorException()
            : base("Data Error")
        {
        }

        public DataErrorException(String message)
            : base(message)
        {
        }

        public DataErrorException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
