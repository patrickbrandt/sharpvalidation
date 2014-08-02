using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpValidation
{
    internal class ValidationHelper
    {
        public static readonly Regex EmailRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);
        public static readonly Regex UrlRegex = new Regex(@"((?i:http|https)://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Compiled);
        public static readonly Regex PhoneNumberRegex = new Regex(@"[1-9]\d{2}-\d{3}-\d{4}", RegexOptions.Compiled);
        public static readonly Regex ZipCodeRegex = new Regex(@"(\d{5}-\d{4})|(\d{5})", RegexOptions.Compiled);

        public static string GetFullPropertyName(MemberExpression call)
        {
            string fullCall = call.ToString();
            int indexOfFirstDot = fullCall.IndexOf(".");
            return fullCall.Substring(indexOfFirstDot + 1);
        }

        public static MemberExpression AssertExpressionIsNotNull<T, TResult>(Expression<Func<T, TResult>> propertyToVerify)
        {
            var call = propertyToVerify.Body as MemberExpression;
            if (call == null)
            {
                throw new InvalidOperationException("Expression cannot be null");
            }

            return call;
        }
    }
}