using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpValidation
{
    public class ValidationResult : IValidationLeaf
    {
        private readonly bool _validationState;
        private readonly string _propertyName;

        internal ValidationResult(bool validationState, string propertyName, Dictionary<string, string> errors)
        {
            ErrorDictionary = errors;
            _validationState = validationState;
            _propertyName = propertyName;
        }

        public ValidationResult WithMessage(string message)
        {
            if (!_validationState)
            {
                ErrorDictionary[_propertyName] = message;
            }

            return this;
        }

        public bool AsBoolean()
        {
            return _validationState;
        }

        #region IValidationLeaf Members

        public Dictionary<string, string> ErrorDictionary { get; private set; }

        #endregion
    }
}