using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Intrastructure
{
    public class NotFoundException : Exception
    {
        private static string _null = "NULL";
        private static string _messageFormat = "{0} with key {1} not found.";

        private static string FormatMessage(object obj, object key) => string.Format(_messageFormat, obj.ToString() ?? _null, key?.ToString() ?? _null);

        public NotFoundException(object obj, object key) : base(FormatMessage(obj, key))
        {
        }
    }
}
