using System.ComponentModel.DataAnnotations;

namespace Shared;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class IsAcceptValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var result = false;
        if (value is bool IsAccept)
        {
            if (IsAccept) result = true;
        }
        return result;
    }
}