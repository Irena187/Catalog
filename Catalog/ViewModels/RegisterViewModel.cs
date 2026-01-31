using System.ComponentModel.DataAnnotations;

namespace Catalog.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email адресът е задължителен")]
        [EmailAddress(ErrorMessage = "Невалиден email адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Паролата трябва да бъде поне 6 символа")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Потвърждението е задължително")]
        [DataType(DataType.Password)]
        [Display(Name = "Потвърди парола")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат")]
        public string ConfirmPassword { get; set; }

        // Добавяме това, за да може потребителят в регистрационната форма да добавя роли
        [Required(ErrorMessage = "Изберете роля")]
        [Display(Name = "Роля")]
        public string Role { get; set; }
    }
}