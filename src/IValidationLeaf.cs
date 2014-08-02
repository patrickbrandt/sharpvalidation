using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpValidation
{
    public interface IValidationLeaf
    {
        Dictionary<string, string> ErrorDictionary { get; }
    }
}