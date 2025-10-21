using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeMail.Common.ValidationRules
{
    public class MailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? mail = value as string;

            if (string.IsNullOrEmpty(mail))
                return new ValidationResult(false, "Email cannot be empty.");

            mail = mail.Trim();
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool isMatch = Regex.IsMatch(mail, pattern);
            if (isMatch)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Invalid email format.");
            }
        }
    }
}
