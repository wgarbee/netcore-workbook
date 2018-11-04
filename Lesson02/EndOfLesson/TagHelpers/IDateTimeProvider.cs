using System;
using System.Collections.Generic;
using System.Text;

namespace TagHelpers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
