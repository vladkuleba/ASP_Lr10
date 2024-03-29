using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Lr10.Models
{
    public class ConsultationRegistration
    {
        [Required(ErrorMessage = "Ім'я та прізвище є обов'язковими.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email є обов'язковим.")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Бажана дата консультації є обов'язковою.")]
        [FutureDate(ErrorMessage = "Дата має бути в майбутньому.")]
        [NonWeekend(ErrorMessage = "Консультація не може відбуватися на вихідні.")]
        public DateTime DesiredConsultationDate { get; set; }

        [Required(ErrorMessage = "Будь ласка, виберіть продукт.")]
        public string Product { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
                return false;
            return ((DateTime)value) > DateTime.Now;
        }
    }

    public class NonWeekendAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is DateTime))
                return new ValidationResult("Невідома дата");

            var date = (DateTime)value;
            var registration = (ConsultationRegistration)validationContext.ObjectInstance;
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return new ValidationResult("Консультація не може відбуватися на вихідні.");
            }

            if (registration.Product == "Основи" && date.DayOfWeek == DayOfWeek.Monday)
            {
                return new ValidationResult("Консультація за темою 'Основи' не може відбуватися по понеділках.");
            }

            return ValidationResult.Success;
        }
    }
}
