using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SharpValidation
{
    public class Validator<T>
    {
        public Dictionary<string, string> ErrorDictionary
        {
            get;
            private set;
        }

        private T ObjectToValidate
        {
            get;
            set;
        }

        public Validator(T objectToValidate) : this(objectToValidate, null) { }

        public Validator(T objectToValidate, Dictionary<string, string> errorDictionary)
        {
            ErrorDictionary = errorDictionary ?? new Dictionary<string, string>();
            ObjectToValidate = objectToValidate;
        }

        public ValidationAction<T, TResult> Validate<TResult>(Expression<Func<T, TResult>> propertyToVerify)
        {
            var call = ValidationHelper.AssertExpressionIsNotNull(propertyToVerify);            

            TResult value = propertyToVerify.Compile()(ObjectToValidate);

            return new ValidationAction<T, TResult>(value, ObjectToValidate, call, ErrorDictionary);
        }           
    }
}