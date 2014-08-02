using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpValidation
{
    public static class ValidationActionExtensions
    {
        public static ValidationResult IsNotEmpty<T>(this ValidationAction<T, string> validationAction)
        {
            return validationAction.GenerateResult(!string.IsNullOrEmpty(validationAction.PropertyValue) && !string.IsNullOrEmpty(validationAction.PropertyValue.Trim()));
        }

        public static ValidationResult IsNotNull<T, TResult>(this ValidationAction<T, TResult> validationAction)
        {
            return validationAction.GenerateResult(validationAction.PropertyValue != null);
        }

        public static ValidationResult IsDateTime<T>(this ValidationAction<T, string> validationAction)
        {
            DateTime tryParse;
            bool isValid = DateTime.TryParse(validationAction.PropertyValue, out tryParse) &&
                           tryParse.Year > 1753;

            return validationAction.GenerateResult(isValid);
        }

        public static ValidationResult IsMoney<T>(this ValidationAction<T, string> validationAction)
        {
            double tryParse;
            bool isValid = double.TryParse(validationAction.PropertyValue, out tryParse);
            return validationAction.GenerateResult(isValid);
        }

        public static ValidationResult IsEmailFormat<T>(this ValidationAction<T, string> validationAction)
        {
            return validationAction.GenerateResult(!string.IsNullOrEmpty(validationAction.PropertyValue) 
                && Regex.IsMatch(validationAction.PropertyValue, ValidationHelper.EmailRegex.ToString()));
        }

        public static ValidationResult IsUrlFormat<T>(this ValidationAction<T, string> validationAction)
        {
            return validationAction.GenerateResult(!string.IsNullOrEmpty(validationAction.PropertyValue) && 
                Regex.IsMatch(validationAction.PropertyValue, ValidationHelper.UrlRegex.ToString()));
        }

        public static ValidationResult IsPhoneNumberFormat<T>(this ValidationAction<T, string> validationAction)
        {
            return validationAction.GenerateResult(!string.IsNullOrEmpty(validationAction.PropertyValue) &&
                Regex.IsMatch(validationAction.PropertyValue, ValidationHelper.PhoneNumberRegex.ToString()));
        }

        public static ValidationResult IsZipCodeFormat<T>(this ValidationAction<T, string> validationAction)
        {
            return validationAction.GenerateResult(!string.IsNullOrEmpty(validationAction.PropertyValue) &&
                Regex.IsMatch(validationAction.PropertyValue, ValidationHelper.ZipCodeRegex.ToString()));
        }

        public static ValidationResult IsNumeric<T>(this ValidationAction<T, string> validationAction)
        {
            int tryInt;
            return validationAction.GenerateResult(int.TryParse(validationAction.PropertyValue, out tryInt));
        }

        public static ValidationResult IsEqualTo<T, TResult>(this ValidationAction<T, TResult> validationAction, Expression<Func<T, TResult>> propertyToVerify)
        {
            ValidationHelper.AssertExpressionIsNotNull(propertyToVerify);

            TResult value = propertyToVerify.Compile()(validationAction.ObjectToValidate);

            return validationAction.GenerateResult(validationAction.PropertyValue != null && validationAction.PropertyValue.Equals(value));
        }

        public static ValidationResult IsNotEqualTo<T, TResult>(this ValidationAction<T, TResult> validationAction, TResult defaultValue)
        {
            return validationAction.GenerateResult(validationAction.PropertyValue != null && !validationAction.PropertyValue.Equals(defaultValue));
        }

        public static ValidationResult MatchesRegex<T>(this ValidationAction<T, string> validationAction, Regex expression)
        {
            return validationAction.GenerateResult(!string.IsNullOrEmpty(validationAction.PropertyValue) && expression.IsMatch(validationAction.PropertyValue));
        }
    }
}
