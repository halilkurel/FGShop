using FGShop.BussinessLayer.Interfaces;
using FGShop.DtoLayer.AuthDtos;
using Microsoft.AspNetCore.Identity;
using FGShop.EntityLayer.Entities;
using FGShop.DtoLayer.AboutDtos;
using FluentValidation;
using FGShop.CommanLayer;
using FluentValidation.Results;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FGShop.BussinessLayer.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidator<UserRegisterDto> _registerValidator;
        private readonly IValidator<UserLoginDto> _loginValidator;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IValidator<UserRegisterDto> registerValidator, IValidator<UserLoginDto> loginValidator, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _tokenService = tokenService;
        }

        public async Task<Response<UserRegisterDto>> Register(UserRegisterDto dto)
        {
            // DTO'yu doğrula
            var validationResponse = _registerValidator.Validate(dto);

            // E-posta veya kullanıcı adı kontrolü
            var existingUserByEmail = await _userManager.FindByEmailAsync(dto.Email);
            var existingUserByUsername = await _userManager.FindByNameAsync(dto.Username);

            if (existingUserByEmail != null)
            {
                return new Response<UserRegisterDto>(ResponseType.ValidationError, dto,
                    new List<CustomValidationError>
                    {
                new CustomValidationError
                {
                    ErrorMessage = "Bu e-posta adresiyle zaten bir kullanıcı mevcut.",
                    PropertyName = "Email"
                }
                    });
            }

            if (existingUserByUsername != null)
            {
                return new Response<UserRegisterDto>(ResponseType.ValidationError, dto,
                    new List<CustomValidationError>
                    {
                new CustomValidationError
                {
                    ErrorMessage = "Bu kullanıcı adıyla zaten bir kullanıcı mevcut.",
                    PropertyName = "Username"
                }
                    });
            }

            // Doğrulama başarısız ise hata dön
            if (!validationResponse.IsValid)
            {
                return new Response<UserRegisterDto>(ResponseType.ValidationError, dto,
                    validationResponse.CovertToCustomValidationError());
            }

            // Yeni kullanıcı oluşturma
            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                // Kullanıcıya rol atama
                var roleResult = await _userManager.AddToRoleAsync(user, "User"); // "User" rolünü değiştirebilirsiniz

                if (!roleResult.Succeeded)
                {
                    // Eğer rol atama başarısız olursa, hata mesajlarını döndür
                    var roleErrors = roleResult.Errors.Select(e => new CustomValidationError
                    {
                        ErrorMessage = e.Description,
                        PropertyName = e.Code // Hata kodunu property ile eşleştirin
                    }).ToList();

                    return new Response<UserRegisterDto>(ResponseType.ValidationError, dto, roleErrors);
                }

                return new Response<UserRegisterDto>(ResponseType.Success, dto);
            }

            // Eğer kullanıcı oluşturulamadıysa, hata mesajlarını döndür
            var errors = result.Errors.Select(e => new CustomValidationError
            {
                ErrorMessage = e.Description,
                PropertyName = e.Code // Hatanın hangi property ile ilgili olduğunu belirtebilirsiniz
            }).ToList();

            return new Response<UserRegisterDto>(ResponseType.ValidationError, dto, errors);
        }



        public async Task<Response<UserLoginDto>> Login(UserLoginDto dto)
        {
            // Validasyonu kontrol et
            var validationResponse = _loginValidator.Validate(dto);

            if (!validationResponse.IsValid)
            {
                // Validasyon hataları varsa döndür
                return new Response<UserLoginDto>(ResponseType.ValidationError, dto,
                    validationResponse.CovertToCustomValidationError());
            }

            // Kullanıcı girişini dene
            var signInResult = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (signInResult.Succeeded)
            {
                var existingUserByUsername = await _userManager.FindByNameAsync(dto.UserName);

                var userRoles = await _userManager.GetRolesAsync(existingUserByUsername);
                if (userRoles != null && userRoles.Contains("User"))
                {
                    var token = _tokenService.TokenCreateUser(dto);
                    return new Response<UserLoginDto>(ResponseType.Success, dto, token);
                }
                else
                {
                    var token = _tokenService.TokenCreateAdmin(dto);
                    return new Response<UserLoginDto>(ResponseType.Success, dto, token);
                }

                
            }

            // Giriş başarısız olursa uygun hata mesajları döndür
            var errors = new List<CustomValidationError>();

            if (signInResult.IsLockedOut)
            {
                errors.Add(new CustomValidationError
                {
                    PropertyName = "UserName",
                    ErrorMessage = "Hesabınız kilitlenmiş. Lütfen daha sonra tekrar deneyin."
                });
            }
            else if (signInResult.IsNotAllowed)
            {
                errors.Add(new CustomValidationError
                {
                    PropertyName = "UserName",
                    ErrorMessage = "Giriş yapmanıza izin verilmiyor."
                });
            }
            else
            {
                errors.Add(new CustomValidationError
                {
                    PropertyName = "UserName",
                    ErrorMessage = "Geçersiz kullanıcı adı veya şifre."
                });
            }

            return new Response<UserLoginDto>(ResponseType.ValidationError, dto, errors);
        }


        public async Task<List<UserListDto>> ListUsers()
        {
            var response = await _userManager.Users.ToListAsync();
            var users = new List<UserListDto>();
            // Her kullanıcıyı UserListDto'ya dönüştür ve listeye ekle
            foreach (var user in response)
            {
                var userDto = new UserListDto
                {
                    Id = user.Id, 
                    Name = user.Name,
                    Surname= user.Surname,
                    Username = user.UserName, 
                    Email = user.Email ,
                    PhoneNumber = user.PhoneNumber 

                };

                users.Add(userDto); 
            }
            return users;
        }

        public async Task<UserListDto> GetUserList(int id)
        {
            var response = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(response == null)
            {
                return null;
            }
            var user = new UserListDto
            {
                Id = response.Id,
                Name = response.Name,
                Surname = response.Surname,
                Username = response.UserName,
                Email = response.Email ,
                PhoneNumber = response.PhoneNumber
            };
            return user;
        }
    }
}
