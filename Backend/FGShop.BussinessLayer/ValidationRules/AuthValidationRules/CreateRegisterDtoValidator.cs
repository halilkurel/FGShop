using FGShop.DtoLayer.AuthDtos;
using FluentValidation;

namespace FGShop.BussinessLayer.ValidationRules.AuthValidationRules
{
	public class CreateRegisterDtoValidator : AbstractValidator<UserRegisterDto>
	{
		public CreateRegisterDtoValidator()
		{
			// Name
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Ad alanı boş olamaz.")
				.MaximumLength(50).WithMessage("Ad alanı en fazla 50 karakter olabilir.");

			// Surname
			RuleFor(x => x.Surname)
				.NotEmpty().WithMessage("Soyad alanı boş olamaz.")
				.MaximumLength(50).WithMessage("Soyad alanı en fazla 50 karakter olabilir.");

			// Username
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
				.MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

			// PhoneNumber (Optional field)
			RuleFor(x => x.PhoneNumber)
				.Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Geçerli bir telefon numarası giriniz.")
				.When(x => !string.IsNullOrEmpty(x.PhoneNumber));

			// Email
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email alanı boş olamaz.")
				.EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

			// Password
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Şifre alanı boş olamaz.")
				.MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
				.Matches(@"[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
				.Matches(@"[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
				.Matches(@"[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");

			// Confirm Password
			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor.")
				.NotEmpty().WithMessage("Şifre doğrulama alanı boş olamaz.");
		}
	}
}
