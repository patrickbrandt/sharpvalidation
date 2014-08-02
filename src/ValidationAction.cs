using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpValidation
{
    public class ValidationAction<T, TResult> : IValidationLeaf
    {        
        public TResult PropertyValue { get; private set; }
        private MemberExpression Property { get; set; }
        public T ObjectToValidate { get; private set; }

        internal ValidationAction(TResult propertyValue, T objectToValidate, MemberExpression property, Dictionary<string, string> errors)
        {
            PropertyValue = propertyValue;
            Property = property;
            ObjectToValidate = objectToValidate;
            ErrorDictionary = errors;            
        } 

        public ValidationResult GenerateResult(bool isValid)
        {
            if (!isValid)
            {
                ErrorDictionary[ValidationHelper.GetFullPropertyName(Property)] = ValidationHelper.GetFullPropertyName(Property) + " has errors";
            }

            return new ValidationResult(isValid, ValidationHelper.GetFullPropertyName(Property), ErrorDictionary);
        }

        #region IValidationLeaf Members

        public Dictionary<string, string> ErrorDictionary { get; private set; }

        #endregion
    }
}