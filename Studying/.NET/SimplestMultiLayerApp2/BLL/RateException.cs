using System;

namespace BLL
{
    public class RateException: Exception
    {
        public RateException() : base() { }
        public RateException(string msg) : base(msg) { }
    }
}
