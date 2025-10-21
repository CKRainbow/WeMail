using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WeMail.Common.ValidationRules
{
    public class PhoneNumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return new ValidationResult(false, "Phone number cannot be empty.");
                }
                str = str.Trim();
                // Simple phone number pattern (you can adjust it as needed)
                string pattern = @"^\+?[1-9]\d{1,14}$";
                bool isMatch = System.Text.RegularExpressions.Regex.IsMatch(str, pattern);
                if (isMatch)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Invalid phone number format.");
                }
            }
            return new ValidationResult(false, "Invalid input type.");
        }
    }
}
