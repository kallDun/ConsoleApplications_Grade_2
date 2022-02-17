using System;
using System.Globalization;
using System.Windows.Controls;

namespace CalcMethodLab1.Logic
{
    class ValidationDoubleRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (((string)value).Length > 0)
                {
                    var number = double.Parse((string)value);
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            return ValidationResult.ValidResult;
        }
    }
}