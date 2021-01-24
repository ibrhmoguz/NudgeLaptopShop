using System;

namespace Nudge.LaptopShop.Api.Common
{
    public class PrimaryKeyViolationException : Exception
    {
        public PrimaryKeyViolationException(string message)
            : base(message)
        {
        }
    }
}
