﻿using System;

namespace Palmtree.Text
{
    public class ShiftJisEncodingException
        : Exception
    {
        public ShiftJisEncodingException()
        {
        }

        public ShiftJisEncodingException(String message)
            : base(message)
        {
        }

        public ShiftJisEncodingException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
