using EBookMasterWebApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace EBookMasterWebApi.Attributes
{
	public class AllowedBookStatusAttribute : ValidationAttribute
	{
		private readonly BookStatus[] _allowedStatuses;

		public AllowedBookStatusAttribute(params BookStatus[] allowedStatuses)
		{
			_allowedStatuses = allowedStatuses;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is BookStatus status && Array.Exists(_allowedStatuses, s => s == status))
				return ValidationResult.Success;

			return new ValidationResult($"Invalid BookStatus value. Allowed values are: {string.Join(", ", _allowedStatuses)}");
		}
	}
}
