using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Domain.User;
using SwordTech.Melodart.Helper.Error;
using SwordTech.Melodart.Helper.Mail;

namespace SwordTech.Melodart.Application.Users;

public class AuthAppAppService : IAuthAppService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserAppService _userAppService;
    private readonly IMemoryCache _memoryCache;

    private readonly IValidator<LoginRequest> _validator;

    public AuthAppAppService(UserManager<AppUser> userManager, IUserAppService userAppService, IMemoryCache memoryCache, IValidator<LoginRequest> validator)
    {
        _userManager = userManager;
        _userAppService = userAppService;
        _memoryCache = memoryCache;
        _validator = validator;
    }

    public async Task<UserDto> Login(LoginRequest loginRequest)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            throw new BusinessException("Validasyon hatası.");
        }

        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            throw new BusinessException("Geçersiz Kullanıcı adı.");
        }

        var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!result)
        {
            throw new BusinessException("Geçersiz parola.");
        }

        return await _userAppService.GetById(user.Id);
    }

    public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);

        if (user == null)
        {
            throw new BusinessException("Geçersiz Kullanıcı adı.");
        }

        // 4 haneli bir kod oluştur ve mail gönder. 

        string code = (new Random()).Next(1000, 9999).ToString();

        var mailService = new MailService(
            "mail.swordtech.net", // SMTP Sunucusu
            587, // Port
            "test@swordtech.net", // Gmail Adresiniz
            "r6Ba6Gm3D" // Gmail Şifreniz veya Uygulama Şifresi 
        );

        await mailService.SendMail(new Mail()
        {
            Title = "Şifre sıfırlama",
            Subject = "Lütfer aşağıdaki kodu kullanın",
            Body = $"Şifre sıfırlama işlemi için kullanabileceğiniz kod:{code}",
            Email = user.Email
        });

        _memoryCache.Set(code, user);
    }

    public async Task<bool> CheckPasswordCode(CheckPasswordCodeRequest checkPasswordCodeRequest)
    {
        AppUser user;

        if (_memoryCache.TryGetValue(checkPasswordCodeRequest.Code, out user))
        {
            return true;
        }

        throw new BusinessException("Geçersiz Kod");
    }

    public async Task<bool> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        AppUser user;

        if (_memoryCache.TryGetValue(changePasswordRequest.Code, out user))
        {
            user = await _userManager.FindByIdAsync(user.Id.ToString());

            if (user == null)
            {
                throw new BusinessException("Kullanıcı bulunamadı. Lütfen tekrar deneyiniz.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, changePasswordRequest.NewPassword);

            if (!result.Succeeded)
            {
                throw new BusinessException("Şifre güncelleme işlemi yapılamadı. Lütfen tekrar deneyiniz.");
            }

            return result.Succeeded;
        }

        throw new BusinessException("Geçersiz Kod");
    }
}
